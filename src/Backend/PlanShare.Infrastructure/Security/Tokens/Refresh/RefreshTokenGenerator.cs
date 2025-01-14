using PlanShare.Domain.Security.Tokens;

namespace PlanShare.Infrastructure.Security.Tokens.Refresh;

internal sealed class RefreshTokenGenerator : IRefreshTokenGenerator
{
    public string Generate() => Convert.ToBase64String(Guid.NewGuid().ToByteArray());
}
