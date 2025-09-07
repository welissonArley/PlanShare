using FluentValidation.Results;
using Mapster;
using PlanShare.Application.Services.Authentication;
using PlanShare.Communication.Requests;
using PlanShare.Communication.Responses;
using PlanShare.Domain.Extensions;
using PlanShare.Domain.Repositories;
using PlanShare.Domain.Repositories.RefreshToken;
using PlanShare.Domain.Repositories.User;
using PlanShare.Domain.Security.Cryptography;
using PlanShare.Exceptions;
using PlanShare.Exceptions.ExceptionsBase;

namespace PlanShare.Application.UseCases.User.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _repository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenWriteOnlyRepository _refreshTokenRepository;

    public RegisterUserUseCase(
        IUnitOfWork unitOfWork,
        IUserWriteOnlyRepository repository,
        IUserReadOnlyRepository userReadOnlyRepository,
        IPasswordEncripter passwordEncripter,
        ITokenService tokenService,
        IRefreshTokenWriteOnlyRepository refreshTokenRepository)
    {
        _unitOfWork = unitOfWork;
        _userReadOnlyRepository = userReadOnlyRepository;
        _repository = repository;
        _passwordEncripter = passwordEncripter;
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = request.Adapt<Domain.Entities.User>();
        user.Password = _passwordEncripter.Encrypt(request.Password);

        var tokens = _tokenService.GenerateTokens(user);

        await _repository.Add(user);

        await _refreshTokenRepository.Add(new Domain.Entities.RefreshToken
        {
            UserId = user.Id,
            Token = tokens.Refresh,
            AccessTokenId = tokens.AccessTokenId
        });

        await _unitOfWork.Commit();

        return new()
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

    private async Task Validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);

        var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
        if (emailExist)
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));

        if (result.IsValid.IsFalse())
            throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
    }
}