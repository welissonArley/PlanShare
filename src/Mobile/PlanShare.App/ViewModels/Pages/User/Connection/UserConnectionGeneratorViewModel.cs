using CommunityToolkit.Mvvm.ComponentModel;
using PlanShare.App.Models;
using PlanShare.App.Navigation;

namespace PlanShare.App.ViewModels.Pages.User.Connection;

public partial class UserConnectionGeneratorViewModel : ViewModelBase
{
    [ObservableProperty]
    public new ConnectionByCodeStatusPage statusPage;

    public UserConnectionGeneratorViewModel(INavigationService navigationService) : base(navigationService)
    {
    }
}
