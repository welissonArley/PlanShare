using CommunityToolkit.Mvvm.Input;
using PlanShare.App.Navigation;
using PlanShare.App.UseCases.Login.WithGoogle;

namespace PlanShare.App.ViewModels.Pages.OnBording;

public partial class OnBoardingViewModel : ViewModelBase
{
    private readonly ILoginWithGoogleUseCase _loginWithGoogleUseCase;

    public OnBoardingViewModel(INavigationService navigationService, ILoginWithGoogleUseCase loginWithGoogleUseCase) : base(navigationService)
    {
        _loginWithGoogleUseCase = loginWithGoogleUseCase;
    }

    [RelayCommand]
    public async Task LoginWithEmailAndPassword() => await _navigationService.GoToAsync(RoutePages.LOGIN_PAGE);

    [RelayCommand]
    public async Task LoginWithGoogle()
    {
        var result = await _loginWithGoogleUseCase.Execute();
        if (result.IsSuccess)
            await _navigationService.GoToDashboardPage();
    }

    [RelayCommand]
    public async Task RegisterUserAccount() => await _navigationService.GoToAsync(RoutePages.USER_REGISTER_ACCOUNT_PAGE);
}
