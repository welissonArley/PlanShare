namespace PlanShare.Domain.Security.Tokens;
public interface IRefreshTokenGenerator
{
    string Generate();
}
