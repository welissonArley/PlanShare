

namespace PlanShare.App.Data.Storage.SecureStorage.Tokens;

public class TokensStorage : ITokensStorage
{
    private const string ACCESS_TOKEN_KEY = "access_token";
    private const string REFRESH_TOKEN_KEY = "refresh_token";

    public void Clear() => Microsoft.Maui.Storage.SecureStorage.RemoveAll();

    public async Task<Models.ValueObjects.Tokens> Get()
    {
        var accessToken = await Microsoft.Maui.Storage.SecureStorage.GetAsync(ACCESS_TOKEN_KEY);
        var refreshToken = await Microsoft.Maui.Storage.SecureStorage.GetAsync(REFRESH_TOKEN_KEY);

        return new Models.ValueObjects.Tokens(accessToken!, refreshToken!);
    }

    public async Task Save(Models.ValueObjects.Tokens tokens)
    {
        await Microsoft.Maui.Storage.SecureStorage.SetAsync(ACCESS_TOKEN_KEY, tokens.AccessToken);
        await Microsoft.Maui.Storage.SecureStorage.SetAsync(REFRESH_TOKEN_KEY, tokens.RefreshToken);
    }
}
