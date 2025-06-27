using CommunityToolkit.Mvvm.ComponentModel;
using PlanShare.App.Navigation;
using PlanShare.App.UseCases.User.Profile;

namespace PlanShare.App.ViewModels.Pages.User.Profile;

public partial class UserProfileViewModel : ViewModelBase
{
    [ObservableProperty]
    public Models.User model;

    private readonly INavigationService _navigationService;
    private readonly IGetProfileUseCase _getProfileUseCase;

    public UserProfileViewModel(INavigationService navigationService, IGetProfileUseCase getProfileUseCase)
    {
        Model = new Models.User
        {
            Name = "Welisson Arley",
            Email = "welisson@gmail.com"
        };

        _navigationService = navigationService;
        _getProfileUseCase = getProfileUseCase;
    }
}
