using CommunityToolkit.Mvvm.Input;
using PlanShare.App.Navigation;

namespace PlanShare.App.ViewModels.Pages.OnBording;

public partial class OnBoardingViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    public OnBoardingViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    public async Task LoginWithEmailAndPassword() => await _navigationService.GoToAsync(RoutePages.LOGIN_PAGE);

    [RelayCommand]
    public void LoginWithGoogle()
    {

    }

    [RelayCommand]
    public async Task RegisterUserAccount() => await _navigationService.GoToAsync(RoutePages.USER_REGISTER_ACCOUNT_PAGE);
}
