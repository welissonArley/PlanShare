using Microsoft.AspNetCore.SignalR;

namespace PlanShare.Api.Hubs;

public class UserConnectionsHub : Hub
{
    public string GenerateCode()
    {
        var code = "1234";

        return code;
    }

    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }
}
