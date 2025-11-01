using Microsoft.AspNetCore.SignalR.Client;
using PlanShare.App.Data.Network.Api;
using PlanShare.App.Data.Storage.SecureStorage.Tokens;
using System.Globalization;

namespace PlanShare.App.Data.Network.Hubs;

public class UserConnectionByCodeClient : IUserConnectionByCodeClient
{
    private readonly ITokensStorage _tokensStorage;
    private readonly string _urlBase;

    public UserConnectionByCodeClient(string urlBase, ITokensStorage tokensStorage)
    {
        _tokensStorage = tokensStorage;
        _urlBase = urlBase;
    }

    public HubConnection CreateClient()
    {
        return new HubConnectionBuilder()
            .WithUrl($"{_urlBase}/connection", options =>
            {
                options.Headers.Add("Accept-Language", CultureInfo.CurrentCulture.Name);

                options.AccessTokenProvider = async () =>
                {
                    var tokens = await _tokensStorage.Get();

                    return tokens.AccessToken;
                };
            })
            .Build();
    }
}
