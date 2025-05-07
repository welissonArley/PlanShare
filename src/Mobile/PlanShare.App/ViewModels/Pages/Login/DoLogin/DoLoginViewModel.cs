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
    private readonly INavigationService _navigationService;

    public DoLoginViewModel(IDoLoginUseCase loginUseCase, INavigationService navigationService)
    {
        Model = new Models.Login();

        _loginUseCase = loginUseCase;
        _navigationService = navigationService;
    }

    [RelayCommand]
    public async Task DoLogin()
    {
        StatusPage = Models.StatusPage.Sending;

        var result = await _loginUseCase.Execute(Model);
        if (result.IsSuccess == false)
        {
            var parameters = new Dictionary<string, object>
            {
                { "errors", result.ErrorMessages! }
            };

            await _navigationService.GoToAsync(RoutePages.ERROR_PAGE, parameters);
        }

        StatusPage = Models.StatusPage.Default;
    }
}
