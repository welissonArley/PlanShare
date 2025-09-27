using Microsoft.AspNetCore.SignalR;
using PlanShare.Api.Hubs.Services;
using PlanShare.Application.UseCases.User.Connection.GenerateCode;

namespace PlanShare.Api.Hubs;

public class UserConnectionsHub : Hub
{
    private readonly IGenerateCodeUserConnectionUseCase _generateCodeUserConnectionUseCase;
    private readonly CodeConnectionService _codeConnectionService;

    public UserConnectionsHub(
        IGenerateCodeUserConnectionUseCase generateCodeUserConnectionUseCase,
        CodeConnectionService codeConnectionService)
    {
        _generateCodeUserConnectionUseCase = generateCodeUserConnectionUseCase;
        _codeConnectionService = codeConnectionService;
    }

    public async Task<string> GenerateCode()
    {
        var codeUserConnectionDto = await _generateCodeUserConnectionUseCase.Execute();

        _codeConnectionService.Start(codeUserConnectionDto, Context.ConnectionId);

        return codeUserConnectionDto.Code;
    }
}
