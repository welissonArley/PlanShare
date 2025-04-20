namespace PlanShare.App.Data.Storage.SecureStorage.Tokens;

public class TokensStorageForVirtualDevice : ITokensStorage
{
    private const string ACCESS_TOKEN_KEY = "access_token";
    private const string REFRESH_TOKEN_KEY = "refresh_token";

    public void Clear() => Microsoft.Maui.Storage.Preferences.Clear();

    public Task<Models.ValueObjects.Tokens> Get()
    {
        var accessToken = Microsoft.Maui.Storage.Preferences.Get(ACCESS_TOKEN_KEY, string.Empty);
        var refreshToken = Microsoft.Maui.Storage.Preferences.Get(REFRESH_TOKEN_KEY, string.Empty);

        var tokens = new Models.ValueObjects.Tokens(accessToken!, refreshToken!);

        return Task.FromResult(tokens);
    }

    public Task Save(Models.ValueObjects.Tokens tokens)
    {
        Microsoft.Maui.Storage.Preferences.Set(ACCESS_TOKEN_KEY, tokens.AccessToken);
        Microsoft.Maui.Storage.Preferences.Set(REFRESH_TOKEN_KEY, tokens.RefreshToken);

        return Task.CompletedTask;
    }
}
