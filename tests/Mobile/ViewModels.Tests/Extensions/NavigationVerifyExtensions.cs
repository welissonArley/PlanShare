using Moq;
using PlanShare.App.Navigation;

namespace ViewModels.Tests.Extensions;
public static class NavigationVerifyExtensions
{
    public static void VerifyGoTo(
        this Mock<INavigationService> navigationService,
        string route,
        IList<string> errorMessages,
        Func<Times> time)
    {
        navigationService.Verify(service =>
            service.GoToAsync(
                IsMatchingShellNavigationState(route),
                IsMatchingErrorDictionaryParameter(errorMessages)
            ), time);
    }

    public static void VerifyGoTo(
        this Mock<INavigationService> navigationService,
        string route,
        Func<Times> time)
    {
        navigationService.Verify(service => service.GoToAsync(IsMatchingShellNavigationState(route)), time);
    }

    private static ShellNavigationState IsMatchingShellNavigationState(string route)
    {
        return It.Is<ShellNavigationState>(state => state.Location.OriginalString.Equals(route));
    }

    private static Dictionary<string, object> IsMatchingErrorDictionaryParameter(IList<string> errorMessages)
    {
        return It.Is<Dictionary<string, object>>(dictionary =>
            dictionary.ContainsKey("errors")
            && dictionary["errors"] is IList<string>
            && ((IList<string>)dictionary["errors"]).Count == errorMessages.Count
            && ((IList<string>)dictionary["errors"]).All(errorMessages.Contains));
    }
}
