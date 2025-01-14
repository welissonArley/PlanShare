using PlanShare.Application.Services.Authentication;
using PlanShare.Communication.Requests;
using PlanShare.Communication.Responses;
using PlanShare.Domain.Repositories.User;
using PlanShare.Domain.Security.Cryptography;
using PlanShare.Exceptions.ExceptionsBase;

namespace PlanShare.Application.UseCases.Login.DoLogin;
public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly ITokenService _tokenService;

    public DoLoginUseCase(
        IUserReadOnlyRepository repository,
        IPasswordEncripter passwordEncripter,
        ITokenService tokenService)
    {
        _passwordEncripter = passwordEncripter;
        _repository = repository;
        _tokenService = tokenService;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var user = await _repository.GetUserByEmail(request.Email);

        if (user is null)
            throw new InvalidLoginException();

        var passwordMatch = _passwordEncripter.IsValid(request.Password, user.Password);

        if (passwordMatch == false)
            throw new InvalidLoginException();

        var tokens = await _tokenService.GenerateTokens(user);

        return new ResponseRegisteredUserJson
        {
            Id = user.Id,
            Name = user.Name,
            Tokens = new ResponseTokensJson
            {
                AccessToken = tokens.Access
            }
        };
    }
}