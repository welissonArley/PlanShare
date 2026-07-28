using PlanShare.Application.Services.Authentication;
using PlanShare.Communication.Responses;
using PlanShare.Domain.Dtos;
using PlanShare.Domain.Repositories;
using PlanShare.Domain.Repositories.RefreshToken;
using PlanShare.Domain.Repositories.User;

namespace PlanShare.Application.UseCases.Login.WithGoogle;

public class LoginWithGoogleUseCase : ILoginWithGoogleUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IUserWriteOnlyRepository _repositoryWrite;
    private readonly IRefreshTokenWriteOnlyRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public LoginWithGoogleUseCase(
        IUserReadOnlyRepository repository,
        IUserWriteOnlyRepository repositoryWrite,
        IUnitOfWork unitOfWork,
        ITokenService tokenService,
        IRefreshTokenWriteOnlyRepository refreshTokenRepository)
    {
        _repository = repository;
        _repositoryWrite = repositoryWrite;
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<ResponseRegisteredUserJson> Execute(LoginThirdPartNameEmailDto info)
    {
        var user = await _repository.GetUserByEmail(info.Email);

        if (user is null)
        {
            user = new Domain.Entities.User
            {
                Email = info.Email,
                Name = info.Name,
                Password = "-"
            };

            await _repositoryWrite.Add(user);
        }

        var tokens = _tokenService.GenerateTokens(user);

        await _refreshTokenRepository.Add(new Domain.Entities.RefreshToken
        {
            UserId = user.Id,
            Token = tokens.Refresh,
            AccessTokenId = tokens.AccessTokenId
        });

        await _unitOfWork.Commit();

        return new ResponseRegisteredUserJson
        {
            Id = user.Id,
            Name = user.Name,
            Tokens = new()
            {
                AccessToken = tokens.Access,
                RefreshToken = tokens.Refresh
            }
        };
    }
}
