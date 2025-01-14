using PlanShare.Domain.Entities;

namespace PlanShare.Domain.Security.Tokens;
public interface IAccessTokenGenerator
{
    (string token, Guid accessTokenIdentifier) Generate(User user);
}
