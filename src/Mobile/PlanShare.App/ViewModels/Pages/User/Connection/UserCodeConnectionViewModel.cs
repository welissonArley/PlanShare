using CommunityToolkit.Mvvm.Input;
using PlanShare.App.Navigation;

namespace PlanShare.App.ViewModels.Pages.User.Connection;
public partial class UserCodeConnectionViewModel : ViewModelBase
{
    public UserCodeConnectionViewModel(INavigationService navigationService) : base(navigationService)
    {
    }

    [RelayCommand]
    public async Task UserCompletedCode(string code)
    {
        //do something with the code response
    }
}
