using PlanShare.Domain.Dtos;
using PlanShare.Domain.Entities;
using PlanShare.Domain.Security.Tokens;

namespace PlanShare.Application.Services.Authentication;
public class TokenService : ITokenService
{
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;

    public TokenService(
        IAccessTokenGenerator accessTokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator)
    {
        _accessTokenGenerator = accessTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
    }

    public TokensDto GenerateTokens(User user)
    {
        (var accessToken, var accessTokenIdentifier) = _accessTokenGenerator.Generate(user);
        var refreshToken = _refreshTokenGenerator.Generate();

        return new TokensDto
        {
            Access = accessToken,
            Refresh = refreshToken,
            AccessTokenId = accessTokenIdentifier
        };
    }
}
