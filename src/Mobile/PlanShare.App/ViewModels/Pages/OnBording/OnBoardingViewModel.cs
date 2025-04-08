using CommunityToolkit.Mvvm.Input;
using PlanShare.App.Navigation;

namespace PlanShare.App.ViewModels.Pages.OnBording;

public partial class OnBoardingViewModel : ViewModelBase
{
    [RelayCommand]
    public async Task LoginWithEmailAndPassword() => await Shell.Current.GoToAsync(RoutePages.LOGIN_PAGE);

    [RelayCommand]
    public void LoginWithGoogle()
    {

    }

    [RelayCommand]
    public async Task RegisterUserAccount() => await Shell.Current.GoToAsync(RoutePages.USER_REGISTER_ACCOUNT_PAGE);
}
