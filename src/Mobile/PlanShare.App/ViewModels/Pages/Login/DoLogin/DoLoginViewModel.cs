using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanShare.App.Navigation;
using PlanShare.App.UseCases.Login.DoLogin;

namespace PlanShare.App.ViewModels.Pages.Login.DoLogin;

public partial class DoLoginViewModel : ViewModelBase
{
    [ObservableProperty]
    public Models.Login model;

    private readonly IDoLoginUseCase _loginUseCase;

    public DoLoginViewModel(INavigationService navigationService, IDoLoginUseCase loginUseCase) : base(navigationService)
    {
        Model = new Models.Login();

        _loginUseCase = loginUseCase;
    }

    [RelayCommand]
    public async Task DoLogin()
    {
        StatusPage = Models.StatusPage.Sending;

        var result = await _loginUseCase.Execute(Model);
        if (result.IsSuccess)
            await _navigationService.GoToDashboardPage();
        else
            await GoToPageWithErrors(result);

        StatusPage = Models.StatusPage.Default;
    }
}
