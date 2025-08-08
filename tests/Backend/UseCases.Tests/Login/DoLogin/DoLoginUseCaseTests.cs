using CommonTestUtilities.Authentication;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Security.Cryptography;
using PlanShare.Application.UseCases.Login.DoLogin;
using PlanShare.Communication.Requests;
using PlanShare.Exceptions;
using PlanShare.Exceptions.ExceptionsBase;
using Shouldly;

namespace UseCases.Tests.Login.DoLogin;
public class DoLoginUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        (var user, var password) = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        var result = await useCase.Execute(new RequestLoginJson
        {
            Email = user.Email,
            Password = password
        });

        result.ShouldNotBeNull();
        result.Tokens.ShouldNotBeNull();
        result.Id.ShouldBe(user.Id);
        result.Name.ShouldBe(user.Name);
    }

    [Fact]
    public async Task Error_Invalid_User()
    {
        var request = RequestLoginJsonBuilder.Build();

        var useCase = CreateUseCase();

        var act = async () => { await useCase.Execute(request); };

        var exception = await act.ShouldThrowAsync<InvalidLoginException>();
        exception.ShouldSatisfyAllConditions(exception =>
        {
            exception.GetStatusCode().ShouldBe(System.Net.HttpStatusCode.Unauthorized);
            exception.GetErrorMessages().ShouldSatisfyAllConditions(erros =>
            {
                erros.Count.ShouldBe(1);
                erros.ShouldContain(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID);
            });
        });
    }

    [Fact]
    public async Task Error_Invalid_Password()
    {
        (var user, _) = UserBuilder.Build();

        var request = RequestLoginJsonBuilder.Build();
        request.Email = user.Email;

        var useCase = CreateUseCase(user);

        var act = async () => { await useCase.Execute(request); };

        var exception = await act.ShouldThrowAsync<InvalidLoginException>();
        exception.ShouldSatisfyAllConditions(exception =>
        {
            exception.GetStatusCode().ShouldBe(System.Net.HttpStatusCode.Unauthorized);
            exception.GetErrorMessages().ShouldSatisfyAllConditions(erros =>
            {
                erros.Count.ShouldBe(1);
                erros.ShouldContain(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID);
            });
        });
    }

    private static DoLoginUseCase CreateUseCase(PlanShare.Domain.Entities.User? user = null)
    {
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var userReadOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
        var tokenService = TokenServiceBuilder.Build();

        if (user is not null)
            userReadOnlyRepositoryBuilder.GetUserByEmail(user);

        return new DoLoginUseCase(userReadOnlyRepositoryBuilder.Build(), passwordEncripter, tokenService);
    }
}
