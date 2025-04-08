using PlanShare.App.ViewModels.Pages.Login.DoLogin;

namespace PlanShare.App.Views.Pages.Login.DoLogin;

public partial class DoLoginPage : ContentPage
{
	public DoLoginPage()
	{
		InitializeComponent();

		BindingContext = new DoLoginViewModel();
    }
}