using PlanShare.Domain.Dtos;

namespace WebApi.Tests.Resources;
public class UserIdentityManager
{
    private readonly PlanShare.Domain.Entities.User _user;
    private readonly string _password;
    private readonly TokensDto _tokens;

    public UserIdentityManager(PlanShare.Domain.Entities.User user, string password, TokensDto tokens)
    {
        _password = password;
        _user = user;
        _tokens = tokens;
    }

    public string GetPassword() => _password;
    public string GetEmail() => _user.Email;
    public string GetName() => _user.Name;
    public string GetAccessToken() => _tokens.Access;
    public string GetRefreshToken() => _tokens.Refresh;
}
