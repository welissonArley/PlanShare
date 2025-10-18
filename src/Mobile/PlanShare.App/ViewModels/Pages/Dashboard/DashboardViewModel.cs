using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanShare.App.Data.Storage.Preferences.User;
using PlanShare.App.Models.Enums;
using PlanShare.App.Navigation;
using PlanShare.App.ViewModels.Popups.Connection;

namespace PlanShare.App.ViewModels.Pages.Dashboard;
public partial class DashboardViewModel : ViewModelBase
{
    [ObservableProperty]
    public string userName;

    public DashboardViewModel(IUserStorage userStorage, INavigationService navigationService) : base(navigationService)
    {
        UserName = userStorage.Get().Name;
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
        }
    }

    [RelayCommand]
    public async Task SeeProfile() => await _navigationService.GoToAsync(RoutePages.USER_UPDATE_PROFILE_PAGE);
}