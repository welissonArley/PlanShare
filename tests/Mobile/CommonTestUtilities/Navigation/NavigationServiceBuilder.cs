using Moq;
using PlanShare.App.Navigation;
using PlanShare.App.ViewModels.Popups;

namespace CommonTestUtilities.Navigation;
public class NavigationServiceBuilder
{
    public static Mock<INavigationService> Build() => new Mock<INavigationService>();

    public static Mock<INavigationService> Build<TViewModel, TResult>(TResult result)
        where TViewModel : ViewModelBaseForPopups
        where TResult : notnull
    {
        var mock = new Mock<INavigationService>();

        mock.Setup(navigationService => navigationService.ShowPopup<TViewModel, TResult>())
            .ReturnsAsync(result);

        return mock;
    }
}
