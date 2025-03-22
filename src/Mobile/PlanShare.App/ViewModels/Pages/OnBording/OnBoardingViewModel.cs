using CommunityToolkit.Mvvm.Input;

namespace PlanShare.App.ViewModels.Pages.OnBording;

public partial class OnBoardingViewModel
{
    [RelayCommand]
    public async Task LoginWithEmailAndPassword()
    {
        await Shell.Current.GoToAsync("DoLoginPage");
    }

    [RelayCommand]
    public void LoginWithGoogle()
    {

    }
}
