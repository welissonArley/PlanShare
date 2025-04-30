using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanShare.App.Models;
using PlanShare.App.Navigation;
using PlanShare.App.UseCases.User.Register;

namespace PlanShare.App.ViewModels.Pages.User.Register;

public partial class RegisterUserAccountViewModel : ViewModelBase
{
    [ObservableProperty]
    public UserRegisterAccount model;

    private readonly INavigationService _navigationService;
    private readonly IRegisterUserUseCase _registerUserUseCase;

    public RegisterUserAccountViewModel(INavigationService navigationService, IRegisterUserUseCase registerUserUseCase)
    {
        Model = new UserRegisterAccount();

        _navigationService = navigationService;
        _registerUserUseCase = registerUserUseCase;
    }

    [RelayCommand]
    public async Task RegisterAccount()
    {
        StatusPage = StatusPage.Sending;

        var result = await _registerUserUseCase.Execute(Model);
        if (result.IsSuccess == false)
            await _navigationService.GoToAsync(RoutePages.ERROR_PAGE);

        StatusPage = StatusPage.Default;
    }

    [RelayCommand]
    public async Task GoToLogin() => await _navigationService.GoToAsync($"../{RoutePages.LOGIN_PAGE}");
}
