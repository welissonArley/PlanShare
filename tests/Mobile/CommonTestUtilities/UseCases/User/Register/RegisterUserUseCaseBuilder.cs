using Moq;
using PlanShare.App.Models;
using PlanShare.App.Models.ValueObjects;
using PlanShare.App.UseCases.User.Register;

namespace CommonTestUtilities.UseCases.User.Register;
public class RegisterUserUseCaseBuilder
{
    public static IRegisterUserUseCase Build(Result result)
    {
        var mock = new Mock<IRegisterUserUseCase>();

        mock.Setup(useCase => useCase.Execute(It.IsAny<UserRegisterAccount>()))
            .ReturnsAsync(result);

        return mock.Object;
    }
}
