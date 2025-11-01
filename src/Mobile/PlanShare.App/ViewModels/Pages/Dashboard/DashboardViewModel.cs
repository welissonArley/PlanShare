using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanShare.App.Models.Enums;
using PlanShare.App.Navigation;
using PlanShare.App.ViewModels.Popups.Connection;

namespace PlanShare.App.ViewModels.Pages.Dashboard;
public partial class DashboardViewModel : ViewModelBase
{
    [ObservableProperty]
    public Models.Dashboard dashboard;

    public DashboardViewModel(INavigationService navigationService) : base(navigationService)
    {
        Dashboard = new Models.Dashboard
        {
            UserName = "Welisson Arley",
            ConnectedUsers = new System.Collections.ObjectModel.ObservableCollection<Models.ConnectedUser>
            {
                new Models.ConnectedUser
                {
                    Name = "Alice Johnson",
                },
                new Models.ConnectedUser
                {
                    Name = "Bob Smith",
                },
                new Models.ConnectedUser
                {
                    Name = "Charlie Brown"
                },
                new Models.ConnectedUser
                {
                    Name = "Alice Johnson",
                },
                new Models.ConnectedUser
                {
                    Name = "Bob Smith",
                },
                new Models.ConnectedUser
                {
                    Name = "Charlie Brown"
                },
                new Models.ConnectedUser
                {
                    Name = "Alice Johnson",
                },
                new Models.ConnectedUser
                {
                    Name = "Bob Smith",
                },
                new Models.ConnectedUser
                {
                    Name = "Charlie Brown"
                }
            }
        };
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
}