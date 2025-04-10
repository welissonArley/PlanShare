using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanShare.App.Models;
using PlanShare.App.Navigation;

namespace PlanShare.App.ViewModels.Pages.User.Register;

public partial class RegisterUserAccountViewModel : ViewModelBase
{
    [ObservableProperty]
    public UserRegisterAccount model;

    private readonly INavigationService _navigationService;

    public RegisterUserAccountViewModel(INavigationService navigationService)
    {
        Model = new UserRegisterAccount();

        _navigationService = navigationService;
    }

    [RelayCommand]
    public async Task RegisterAccount()
    {
    }

    [RelayCommand]
    public async Task GoToLogin() => await _navigationService.GoToAsync($"../{RoutePages.LOGIN_PAGE}");
}
