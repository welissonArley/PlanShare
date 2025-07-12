using CommunityToolkit.Mvvm.ComponentModel;
using PlanShare.App.Navigation;

namespace PlanShare.App.ViewModels.Pages.User.ChangePassword;
public partial class ChangeUserPasswordViewModel : ViewModelBase
{
    [ObservableProperty]
    public Models.ChangePassword model;

    public ChangeUserPasswordViewModel(INavigationService navigationService) : base(navigationService)
    {
    }
}
