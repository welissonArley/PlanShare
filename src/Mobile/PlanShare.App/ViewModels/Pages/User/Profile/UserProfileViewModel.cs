using CommunityToolkit.Mvvm.ComponentModel;
using PlanShare.App.Navigation;

namespace PlanShare.App.ViewModels.Pages.User.Profile;

public partial class UserProfileViewModel : ViewModelBase
{
    [ObservableProperty]
    public Models.User model;

    private readonly INavigationService _navigationService;

    public UserProfileViewModel(INavigationService navigationService)
    {
        Model = new Models.User
        {
            Name = "Welisson Arley",
            Email = "welisson@gmail.com"
        };

        _navigationService = navigationService;
    }
}
