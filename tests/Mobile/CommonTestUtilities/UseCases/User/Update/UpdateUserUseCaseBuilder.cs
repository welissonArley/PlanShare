using Moq;
using PlanShare.App.Models.ValueObjects;
using PlanShare.App.UseCases.User.Update;

namespace CommonTestUtilities.UseCases.User.Update;
public class UpdateUserUseCaseBuilder
{
    public static IUpdateUserUseCase Build(Result result)
    {
        var mock = new Mock<IUpdateUserUseCase>();

        mock.Setup(useCase => useCase.Execute(It.IsAny<PlanShare.App.Models.User>())).ReturnsAsync(result);

        return mock.Object;
    }
}
