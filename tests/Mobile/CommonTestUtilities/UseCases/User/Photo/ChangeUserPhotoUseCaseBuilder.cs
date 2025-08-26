using Moq;
using PlanShare.App.Models.ValueObjects;
using PlanShare.App.UseCases.User.Photo;

namespace CommonTestUtilities.UseCases.User.Photo;
public class ChangeUserPhotoUseCaseBuilder
{
    public static IChangeUserPhotoUseCase Build(Result result)
    {
        var mock = new Mock<IChangeUserPhotoUseCase>();

        mock.Setup(useCase => useCase.Execute(It.IsAny<FileResult>())).ReturnsAsync(result);

        return mock.Object;
    }
}
