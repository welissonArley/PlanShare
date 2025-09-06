using PlanShare.Domain.Dtos;
using PlanShare.Domain.Entities;
using PlanShare.Domain.Repositories;
using PlanShare.Domain.Security.Tokens;

namespace PlanShare.Application.Services.Authentication;
public class TokenService : ITokenService
{
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public TokenService(
        IAccessTokenGenerator accessTokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _accessTokenGenerator = accessTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<TokensDto> GenerateTokens(User user)
    {
        (var accessToken, var accessTokenIdentifier) = _accessTokenGenerator.Generate(user);
        var refreshToken = _refreshTokenGenerator.Generate();

        return new TokensDto
        {
            Access = accessToken,
            Refresh = refreshToken
        };
    }
}
