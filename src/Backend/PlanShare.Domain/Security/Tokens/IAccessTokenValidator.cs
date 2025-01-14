namespace PlanShare.Domain.Security.Tokens;
public interface IAccessTokenValidator
{
    void Validate(string token);
    Guid GetUserIdentifier(string token);
    Guid GetAccessTokenIdentifier(string token);
}
