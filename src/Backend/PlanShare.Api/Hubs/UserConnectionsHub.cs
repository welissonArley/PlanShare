using Microsoft.AspNetCore.SignalR;
using PlanShare.Application.UseCases.User.Connection.GenerateCode;

namespace PlanShare.Api.Hubs;

public class UserConnectionsHub : Hub
{
    private readonly IGenerateCodeUserConnectionUseCase _generateCodeUserConnectionUseCase;

    public UserConnectionsHub(IGenerateCodeUserConnectionUseCase generateCodeUserConnectionUseCase)
    {
        _generateCodeUserConnectionUseCase = generateCodeUserConnectionUseCase;
    }

    public async Task<string> GenerateCode()
    {
        var codeUserConnectionDto = await _generateCodeUserConnectionUseCase.Execute();

        return codeUserConnectionDto.Code;
    }

    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }
}
