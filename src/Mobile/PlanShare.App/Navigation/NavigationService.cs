
namespace PlanShare.App.Navigation;

public class NavigationService : INavigationService
{
    public async Task GoToAsync(ShellNavigationState state) => await Shell.Current.GoToAsync(state);
    public async Task GoToAsync(ShellNavigationState route, Dictionary<string, object> parameters)
        => await Shell.Current.GoToAsync(route, parameters);
}
