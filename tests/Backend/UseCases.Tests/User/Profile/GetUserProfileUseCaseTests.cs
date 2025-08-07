using CommonTestUtilities.Entities;
using CommonTestUtilities.Services.LoggedUser;
using PlanShare.Application.UseCases.User.Profile;
using Shouldly;

namespace UseCases.Tests.User.Profile;
public class GetUserProfileUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        (var user, _) = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        var response = await useCase.Execute();

        response.ShouldNotBeNull();
        response.Name.ShouldBe(user.Name);
        response.Email.ShouldBe(user.Email);
    }

    private GetUserProfileUseCase CreateUseCase(PlanShare.Domain.Entities.User user)
    {
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GetUserProfileUseCase(loggedUser);
    }
}
