using PlanShare.Application.Services.Authentication;
using PlanShare.Communication.Requests;
using PlanShare.Communication.Responses;
using PlanShare.Domain.Extensions;
using PlanShare.Domain.Repositories;
using PlanShare.Domain.Repositories.RefreshToken;
using PlanShare.Domain.Repositories.User;
using PlanShare.Domain.Security.Cryptography;
using PlanShare.Exceptions.ExceptionsBase;

namespace PlanShare.Application.UseCases.Login.DoLogin;
public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenWriteOnlyRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DoLoginUseCase(
        IUserReadOnlyRepository repository,
        IPasswordEncripter passwordEncripter,
        ITokenService tokenService,
        IRefreshTokenWriteOnlyRepository refreshTokenRepository,
        IUnitOfWork unitOfWork)
    {
        _passwordEncripter = passwordEncripter;
        _repository = repository;
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var user = await _repository.GetUserByEmail(request.Email);

        if (user is null)
            throw new InvalidLoginException();

        var passwordMatch = _passwordEncripter.IsValid(request.Password, user.Password);
        if (passwordMatch.IsFalse())
            throw new InvalidLoginException();

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
            Tokens = new ResponseTokensJson
            {
                AccessToken = tokens.Access,
                RefreshToken = tokens.Refresh
            }
        };
    }
}