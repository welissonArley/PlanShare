using Moq;
using PlanShare.App.Models.ValueObjects;
using PlanShare.App.UseCases.User.ChangePassword;

namespace CommonTestUtilities.UseCases.User.ChangePassword;
public class ChangeUserPasswordUseCaseBuild
{
    public static IChangeUserPasswordUseCase Build(Result result)
    {
        var mock = new Mock<IChangeUserPasswordUseCase>();

        mock.Setup(useCase => useCase.Execute(It.IsAny<PlanShare.App.Models.ChangePassword>()))
            .ReturnsAsync(result);

        return mock.Object;
    }
}
