using CommonTestUtilities.Authentication;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Security.Cryptography;
using PlanShare.Application.UseCases.User.Register;
using PlanShare.Domain.Extensions;
using PlanShare.Exceptions;
using PlanShare.Exceptions.ExceptionsBase;
using Shouldly;

namespace UseCases.Tests.User.Register;
public class RegisterUserUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserBuilder.Build();

        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.ShouldNotBeNull();
        result.Name.ShouldBe(request.Name);
        result.Id.ShouldNotBe(Guid.Empty);
        result.Tokens.ShouldNotBeNull();
        result.Tokens.AccessToken.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_EmailAlreadyRegistered()
    {
        var request = RequestRegisterUserBuilder.Build();

        var useCase = CreateUseCase(request.Email);

        var act = async () => await useCase.Execute(request);

        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();

        exception.ShouldSatisfyAllConditions(exception =>
        {
            exception.GetStatusCode().ShouldBe(System.Net.HttpStatusCode.BadRequest);
            exception.GetErrorMessages().ShouldSatisfyAllConditions(erros =>
            {
                erros.Count.ShouldBe(1);
                erros.ShouldContain(ResourceMessagesException.EMAIL_ALREADY_REGISTERED);
            });
        });
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestRegisterUserBuilder.Build();
        request.Name = string.Empty;

        var useCase = CreateUseCase();

        var act = async () => await useCase.Execute(request);

        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();

        exception.ShouldSatisfyAllConditions(exception =>
        {
            exception.GetStatusCode().ShouldBe(System.Net.HttpStatusCode.BadRequest);
            exception.GetErrorMessages().ShouldSatisfyAllConditions(erros =>
            {
                erros.Count.ShouldBe(1);
                erros.ShouldContain(ResourceMessagesException.NAME_EMPTY);
            });
        });
    }

    private RegisterUserUseCase CreateUseCase(string? emailAlreadyExist = null)
    {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var userWriteOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var tokenService = TokenServiceBuilder.Build();

        var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder();
        if(emailAlreadyExist.NotEmpty())
            userReadOnlyRepository.ExistActiveUserWithEmail(emailAlreadyExist);

        return new RegisterUserUseCase(unitOfWork, userWriteOnlyRepository, userReadOnlyRepository.Build(), passwordEncripter, tokenService);
    }
}
