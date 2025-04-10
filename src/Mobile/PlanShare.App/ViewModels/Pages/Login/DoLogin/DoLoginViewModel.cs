using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PlanShare.App.ViewModels.Pages.Login.DoLogin;

public partial class DoLoginViewModel : ViewModelBase
{
    [ObservableProperty]
    public Models.Login model;

    public DoLoginViewModel()
    {
        Model = new Models.Login();
    }

    [RelayCommand]
    public async Task DoLogin()
    {

    }
}
