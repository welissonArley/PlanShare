using Microsoft.AspNetCore.SignalR.Client;

namespace PlanShare.App.Data.Network.Api;

public interface IUserConnectionByCodeClient
{
    HubConnection CreateClient();
}
