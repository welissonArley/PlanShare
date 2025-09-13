using Microsoft.Extensions.Options;
using PlanShare.Application.Services.Authentication;
using PlanShare.Communication.Requests;
using PlanShare.Communication.Responses;
using PlanShare.Domain.Repositories;
using PlanShare.Domain.Repositories.RefreshToken;
using PlanShare.Domain.Security.Tokens;
using PlanShare.Exceptions.ExceptionsBase;

namespace PlanShare.Application.UseCases.Token.RefreshToken;
public class UseRefreshTokenUseCase : IUseRefreshTokenUseCase
{
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenWriteOnlyRepository _refreshTokenWriteOnlyRepository;
    private readonly IRefreshTokenReadOnlyRepository _refreshTokenReadOnlyRepository;
    private readonly IAccessTokenValidator _accessTokenValidator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly TokenSettings _tokenSettings;

    public UseRefreshTokenUseCase(
        ITokenService tokenService,
        IRefreshTokenWriteOnlyRepository refreshTokenWriteOnlyRepository,
        IRefreshTokenReadOnlyRepository refreshTokenReadOnlyRepository,
        IAccessTokenValidator accessTokenValidator,
        IUnitOfWork unitOfWork,
        IOptions<TokenSettings> tokenSettings)
    {
        _tokenService = tokenService;
        _refreshTokenWriteOnlyRepository = refreshTokenWriteOnlyRepository;
        _refreshTokenReadOnlyRepository = refreshTokenReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _accessTokenValidator = accessTokenValidator;
        _tokenSettings = tokenSettings.Value;
    }
    
    public async Task<ResponseTokensJson> Execute(RequestNewTokenJson request)
    {
        var refreshToken = await _refreshTokenReadOnlyRepository.Get(request.RefreshToken);
        if (refreshToken is null)
            throw new RefreshTokenNotFoundException();

        var accessTokenId = _accessTokenValidator.GetAccessTokenIdentifier(request.AccessToken);
        if(refreshToken.AccessTokenId != accessTokenId)
            throw new RefreshTokenNotFoundException();

        var expireAt = refreshToken.CreatedAt.AddDays(_tokenSettings.RefreshTokenValidityDays);
        if(DateTime.UtcNow > expireAt)
            throw new RefreshTokenExpiredException();

        var tokens = _tokenService.GenerateTokens(refreshToken.User);

        await _refreshTokenWriteOnlyRepository.Add(new Domain.Entities.RefreshToken
        {
            UserId = refreshToken.UserId,
            Token = tokens.Refresh,
            AccessTokenId = tokens.AccessTokenId
        });

        await _unitOfWork.Commit();

        return new ResponseTokensJson
        {
            RefreshToken = tokens.Refresh,
            AccessToken = tokens.Access
        };
    }
}