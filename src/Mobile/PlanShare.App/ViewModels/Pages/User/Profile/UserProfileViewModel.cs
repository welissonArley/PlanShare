using CommunityToolkit.Mvvm.ComponentModel;
using PlanShare.App.Data.Storage.Preferences.User;
using PlanShare.App.Navigation;

namespace PlanShare.App.ViewModels.Pages.User.Profile;

public partial class UserProfileViewModel : ViewModelBase
{
    [ObservableProperty]
    public string userName;

    private readonly INavigationService _navigationService;

    public UserProfileViewModel(IUserStorage userStorage, INavigationService navigationService)
    {
        UserName = userStorage.Get().Name;

        _navigationService = navigationService;
    }
}
