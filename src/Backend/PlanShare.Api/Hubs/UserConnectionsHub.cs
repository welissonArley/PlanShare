using Microsoft.AspNetCore.SignalR;
using PlanShare.Api.Hubs.Services;
using PlanShare.Application.UseCases.User.Connection.GenerateCode;
using PlanShare.Application.UseCases.User.Connection.JoinWithCode;
using PlanShare.Communication.Responses;

namespace PlanShare.Api.Hubs;

public class UserConnectionsHub : Hub
{
    private readonly IGenerateCodeUserConnectionUseCase _generateCodeUserConnectionUseCase;
    private readonly IJoinWithCodeUseCase _joinWithCodeUseCase;
    private readonly CodeConnectionService _codeConnectionService;

    public UserConnectionsHub(
        IGenerateCodeUserConnectionUseCase generateCodeUserConnectionUseCase,
        IJoinWithCodeUseCase joinWithCodeUseCase,
        CodeConnectionService codeConnectionService)
    {
        _generateCodeUserConnectionUseCase = generateCodeUserConnectionUseCase;
        _codeConnectionService = codeConnectionService;
        _joinWithCodeUseCase = joinWithCodeUseCase;
    }

    public async Task<string> GenerateCode()
    {
        var codeUserConnectionDto = await _generateCodeUserConnectionUseCase.Execute();

        _codeConnectionService.Start(codeUserConnectionDto, Context.ConnectionId);

        return codeUserConnectionDto.Code;
    }

    public async Task JoinWithCode(string code)
    {
        var userConnections = _codeConnectionService.GetConnectionByCode(code);

        var response = await _joinWithCodeUseCase.Execute(userConnections.UserId);

        userConnections.ConnectingUserId = response.Id;
        userConnections.ConnectingUserConnectionId = Context.ConnectionId;

        await Clients.Client(userConnections.UserConnectionId).SendAsync("OnUserJoined", new ResponseConnectingUserJson
        {
            Name = response.Name,
            ProfilePhotoUrl = response.ProfilePhotoUrl
        });
    }
}
