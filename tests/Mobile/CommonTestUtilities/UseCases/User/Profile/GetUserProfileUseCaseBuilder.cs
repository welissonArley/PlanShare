using Moq;
using PlanShare.App.Models.ValueObjects;
using PlanShare.App.UseCases.User.Profile;

namespace CommonTestUtilities.UseCases.User.Profile;
public class GetUserProfileUseCaseBuilder
{
    public static IGetUserProfileUseCase Build(Result<PlanShare.App.Models.User> result)
    {
        var mock = new Mock<IGetUserProfileUseCase>();

        mock.Setup(useCase => useCase.Execute()).ReturnsAsync(result);

        return mock.Object;
    }
}
