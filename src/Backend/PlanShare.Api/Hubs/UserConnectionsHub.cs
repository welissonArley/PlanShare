using Microsoft.AspNetCore.SignalR;
using PlanShare.Api.Hubs.Services;
using PlanShare.Application.UseCases.User.Connection.ApproveCode;
using PlanShare.Application.UseCases.User.Connection.GenerateCode;
using PlanShare.Application.UseCases.User.Connection.JoinWithCode;
using PlanShare.Communication.Enums;
using PlanShare.Communication.Responses;
using PlanShare.Domain.Extensions;
using PlanShare.Exceptions;

namespace PlanShare.Api.Hubs;

public class UserConnectionsHub : Hub
{
    private readonly IGenerateCodeUserConnectionUseCase _generateCodeUserConnectionUseCase;
    private readonly IJoinWithCodeUseCase _joinWithCodeUseCase;
    private readonly IApproveCodeUserConnectionUseCase _approveCodeUserConnectionUseCase;
    private readonly CodeConnectionService _codeConnectionService;

    public UserConnectionsHub(
        IGenerateCodeUserConnectionUseCase generateCodeUserConnectionUseCase,
        IJoinWithCodeUseCase joinWithCodeUseCase,
        IApproveCodeUserConnectionUseCase approveCodeUserConnectionUseCase,
        CodeConnectionService codeConnectionService)
    {
        _generateCodeUserConnectionUseCase = generateCodeUserConnectionUseCase;
        _codeConnectionService = codeConnectionService;
        _joinWithCodeUseCase = joinWithCodeUseCase;
        _approveCodeUserConnectionUseCase = approveCodeUserConnectionUseCase;
    }

    public async Task<HubOperationResult<string>> GenerateCode()
    {
        var codeUserConnectionDto = await _generateCodeUserConnectionUseCase.Execute();

        _codeConnectionService.Start(codeUserConnectionDto, Context.ConnectionId);

        return HubOperationResult<string>.Success(codeUserConnectionDto.Code);
    }

    public async Task<HubOperationResult<string>> JoinWithCode(string code)
    {
        var userConnections = _codeConnectionService.GetConnectionByCode(code);
        if(userConnections is null)
            return HubOperationResult<string>.Failure(ResourceMessagesException.PROVIDED_CODE_DOES_NOT_EXIST, UserConnectionErrorCode.InvalidCode);

        var result = await _joinWithCodeUseCase.Execute(userConnections.UserId);
        if (result.IsSuccess.IsFalse())
            return HubOperationResult<string>.Failure(result.ErrorMessage, result.ErrorCode!.Value);

        userConnections.ConnectingUserId = result.Response!.Connector.Id;
        userConnections.ConnectingUserConnectionId = Context.ConnectionId;

        await Clients.Client(userConnections.UserConnectionId).SendAsync("OnUserJoined", new ResponseConnectingUserJson
        {
            Name = result.Response.Connector.Name,
            ProfilePhotoUrl = result.Response.Connector.ProfilePhotoUrl
        });

        return HubOperationResult<string>.Success(result.Response.Generator.Name);
    }

    public async Task Cancel(string code)
    {
        var connection = _codeConnectionService.RemoveConnection(code);

        if(connection is not null && connection.ConnectingUserId.HasValue)
            await Clients.Client(connection.ConnectingUserConnectionId!).SendAsync("OnCancelled");
    }

    public async Task ConfirmCodeJoin(string code)
    {
        var connection = _codeConnectionService.RemoveConnection(code);

        await _approveCodeUserConnectionUseCase.Execute(connection);

        await Clients.Client(connection.ConnectingUserConnectionId!).SendAsync("OnConnectionConfirmed");
    }
}