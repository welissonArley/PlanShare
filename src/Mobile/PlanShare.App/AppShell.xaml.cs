using PlanShare.App.Data.Storage.Preferences.User;

namespace PlanShare.App;

public partial class AppShell : Shell
{
    public AppShell(IUserStorage userStorage)
    {
        InitializeComponent();

        if(userStorage.IsLoggedIn())
            ShellPlanShareApp.CurrentItem = DashboardSection;
        else
            ShellPlanShareApp.CurrentItem = OnboardingSection;
    }
}
