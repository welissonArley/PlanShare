
namespace PlanShare.App.Navigation;

public class NavigationService : INavigationService
{
    public async Task GoToAsync(ShellNavigationState state) => await Shell.Current.GoToAsync(state);
}
