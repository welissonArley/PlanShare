using Moq;
using PlanShare.App.Models.ValueObjects;
using PlanShare.App.UseCases.Login.DoLogin;

namespace CommonTestUtilities.UseCases.Login.DoLogin;
public class DoLoginUseCaseBuilder
{
    public static IDoLoginUseCase Build(Result result)
    {
        var mock = new Mock<IDoLoginUseCase>();

        mock.Setup(useCase => useCase.Execute(It.IsAny<PlanShare.App.Models.Login>()))
            .ReturnsAsync(result);

        return mock.Object;
    }
}
