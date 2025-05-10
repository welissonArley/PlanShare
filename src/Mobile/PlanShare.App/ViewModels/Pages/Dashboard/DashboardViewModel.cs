using CommunityToolkit.Mvvm.ComponentModel;
using PlanShare.App.Data.Storage.Preferences.User;

namespace PlanShare.App.ViewModels.Pages.Dashboard;
public partial class DashboardViewModel : ViewModelBase
{
    [ObservableProperty]
    public string userName;

    public DashboardViewModel(IUserStorage userStorage)
    {
        UserName = userStorage.Get().Name;
    }
}
