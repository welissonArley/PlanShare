using PlanShare.Application.Services.Authentication;
using PlanShare.Communication.Requests;
using PlanShare.Communication.Responses;
using PlanShare.Domain.Repositories;
using PlanShare.Domain.Repositories.RefreshToken;
using PlanShare.Domain.Security.Tokens;

namespace PlanShare.Application.UseCases.Token.RefreshToken;
public class UseRefreshTokenUseCase : IUseRefreshTokenUseCase
{
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenWriteOnlyRepository _refreshTokenWriteOnlyRepository;
    private readonly IRefreshTokenReadOnlyRepository _refreshTokenReadOnlyRepository;
    private readonly IAccessTokenValidator _accessTokenValidator;
    private readonly IUnitOfWork _unitOfWork;

    public UseRefreshTokenUseCase(
        ITokenService tokenService,
        IRefreshTokenWriteOnlyRepository refreshTokenWriteOnlyRepository,
        IRefreshTokenReadOnlyRepository refreshTokenReadOnlyRepository,
        IAccessTokenValidator accessTokenValidator,
        IUnitOfWork unitOfWork)
    {
        _tokenService = tokenService;
        _refreshTokenWriteOnlyRepository = refreshTokenWriteOnlyRepository;
        _refreshTokenReadOnlyRepository = refreshTokenReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _accessTokenValidator = accessTokenValidator;
    }
    
    public async Task<ResponseTokensJson> Execute(RequestNewTokenJson request)
    {
        return new ResponseTokensJson();
    }
}