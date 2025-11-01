using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanShare.App.Models.Enums;
using PlanShare.App.Navigation;
using PlanShare.App.UseCases.Dashboard;
using PlanShare.App.ViewModels.Popups.Connection;

namespace PlanShare.App.ViewModels.Pages.Dashboard;
public partial class DashboardViewModel : ViewModelBase
{
    [ObservableProperty]
    public Models.Dashboard dashboard;

    private readonly IGetDashboardUseCase _dashboardUseCase;

    public DashboardViewModel(INavigationService navigationService, IGetDashboardUseCase dashboardUseCase) : base(navigationService)
    {
        _dashboardUseCase = dashboardUseCase;
    }

    [RelayCommand]
    public async Task ConnectionByCode()
    {
        var optionSelected = await _navigationService.ShowPopup<OptionsForConnectionByCodeViewModel, ChooseCodeConnectionOption>();
        switch (optionSelected)
        {
            case ChooseCodeConnectionOption.GenerateCode:
                {
                    await _navigationService.GoToAsync(RoutePages.USER_CONNECTION_GENERATOR_PAGE);
                }
                break;
            case ChooseCodeConnectionOption.UseCode:
                {
                    await _navigationService.GoToAsync(RoutePages.USER_CODE_CONNECTION_PAGE);
                }
                break;
        }
    }

    [RelayCommand]
    public async Task SeeProfile() => await _navigationService.GoToAsync(RoutePages.USER_UPDATE_PROFILE_PAGE);

    [RelayCommand]
    public async Task Initialize()
    {
        StatusPage = Models.StatusPage.Loading;

        var result = await _dashboardUseCase.Execute();
        if (result.IsSuccess)
            Dashboard = result.Response!;
        else
            await GoToPageWithErrors(result);

        StatusPage = Models.StatusPage.Default;
    }
}