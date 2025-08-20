using Moq;
using PlanShare.App.Navigation;

namespace CommonTestUtilities.Navigation;
public class NavigationServiceBuilder
{
    public static Mock<INavigationService> Build() => new Mock<INavigationService>();
}
