using System.Windows.Input;

namespace PlanShare.App.ViewModels.Pages.OnBording;

public class OnBoardingViewModel
{
    public ICommand LoginWithEmailAndPasswordCommand { get; set; }

    public OnBoardingViewModel()
    {
        LoginWithEmailAndPasswordCommand = new Command(LoginWithEmailAndPassword);
    }

    public void LoginWithEmailAndPassword()
    {

    }
}
