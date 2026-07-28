using PlanShare.App.Data.Storage.Preferences.User;
using PlanShare.App.Data.Storage.SecureStorage.Tokens;
using PlanShare.App.Models.ValueObjects;

namespace PlanShare.App.UseCases.Login.WithGoogle;

public class LoginWithGoogleUseCase : ILoginWithGoogleUseCase
{
    private readonly IWebAuthenticator _webAuthenticator;
    private readonly IUserStorage _userStorage;
    private readonly ITokensStorage _tokensStorage;
    private readonly string _apiUrlLoginGoogle;

    public LoginWithGoogleUseCase(
        IWebAuthenticator webAuthenticator,
        ITokensStorage tokensStorage,
        IUserStorage userStorage,
        string apiUrlLoginGoogle)
    {
        _webAuthenticator = webAuthenticator;
        _tokensStorage = tokensStorage;
        _userStorage = userStorage;
        _apiUrlLoginGoogle = apiUrlLoginGoogle;
    }

    public async Task<Result> Execute()
    {
        try
        {
            var authResult = await _webAuthenticator
            .AuthenticateAsync(new Uri(_apiUrlLoginGoogle), new Uri("planshare://"));

            var accessToken = authResult.AccessToken;
            var refreshToken = authResult.RefreshToken;
            var name = authResult.Properties["name"];
            var userId = authResult.Properties["id"];

            var user = new Models.ValueObjects.User(Guid.Parse(userId), name);
            var tokens = new Models.ValueObjects.Tokens(accessToken, refreshToken);

            _userStorage.Save(user);
            await _tokensStorage.Save(tokens);

            return Result.Success();
        }
        catch
        {
            return Result.Failure([""]);
        }
    }
}
