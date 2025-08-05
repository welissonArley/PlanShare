using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Services.LoggedUser;
using PlanShare.Application.UseCases.User.Update;
using PlanShare.Domain.Extensions;
using PlanShare.Exceptions;
using PlanShare.Exceptions.ExceptionsBase;
using Shouldly;

namespace UseCases.Tests.User.Update;
public class UpdateUserUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var request = RequestUpdateUserBuilder.Build();

        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        await act.ShouldNotThrowAsync();

        user.Name.ShouldBe(request.Name);
        user.Email.ShouldBe(request.Email);
    }

    [Fact]
    public async Task Error_EmailAlreadyRegistered()
    {
        var user = UserBuilder.Build();
        var request = RequestUpdateUserBuilder.Build();

        var useCase = CreateUseCase(user, request.Email);

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
        var user = UserBuilder.Build();
        var request = RequestUpdateUserBuilder.Build();
        request.Name = string.Empty;

        var useCase = CreateUseCase(user);

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

    private UpdateUserUseCase CreateUseCase(PlanShare.Domain.Entities.User user, string? emailAlreadyExist = null)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var userUpdateOnlyRepository = UserUpdateOnlyRepositoryBuilder.Build(user);
        var unitOfWork = UnitOfWorkBuilder.Build();

        var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder();
        if (emailAlreadyExist.NotEmpty())
            userReadOnlyRepository.ExistActiveUserWithEmail(emailAlreadyExist);

        return new UpdateUserUseCase(loggedUser, userUpdateOnlyRepository, userReadOnlyRepository.Build(), unitOfWork);
    }
}
