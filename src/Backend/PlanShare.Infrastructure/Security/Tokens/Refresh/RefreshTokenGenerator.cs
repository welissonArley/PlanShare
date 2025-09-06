using PlanShare.Domain.Security.Tokens;
using System.Security.Cryptography;

namespace PlanShare.Infrastructure.Security.Tokens.Refresh;

internal sealed class RefreshTokenGenerator : IRefreshTokenGenerator
{
    public string Generate()
    {
        var token = RandomNumberGenerator.GetBytes(32);

        return Convert.ToBase64String(token);
    }
}
