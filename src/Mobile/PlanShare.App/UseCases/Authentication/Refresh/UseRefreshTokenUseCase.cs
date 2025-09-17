using PlanShare.App.Data.Network.Api;
using PlanShare.App.Data.Storage.Preferences.User;
using PlanShare.App.Data.Storage.SecureStorage.Tokens;
using PlanShare.App.Models.ValueObjects;
using PlanShare.Communication.Requests;

namespace PlanShare.App.UseCases.Authentication.Refresh;
public class UseRefreshTokenUseCase : IUseRefreshTokenUseCase
{
    private readonly IAuthenticationApi _authenticationApi;
    private readonly ITokensStorage _tokensStorage;
    private readonly IUserStorage _userStorage;

    public UseRefreshTokenUseCase(
        IAuthenticationApi authenticationApi,
        ITokensStorage tokensStorage,
        IUserStorage userStorage)
    {
        _authenticationApi = authenticationApi;
        _tokensStorage = tokensStorage;
        _userStorage = userStorage;
    }

    public async Task<Result<Tokens>> Execute()
    {
        var tokens = await _tokensStorage.Get();

        var request = new RequestNewTokenJson
        {
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken,
        };

        var response = await _authenticationApi.Refresh(request);

        if (response.IsSuccessful)
        {
            tokens = new Tokens(response.Content.AccessToken, response.Content.RefreshToken);

            await _tokensStorage.Save(tokens);

            return Result<Tokens>.Success(tokens);
        }

        _userStorage.Clear();
        _tokensStorage.Clear();

        return Result<Tokens>.Failure([]);
    }
}
