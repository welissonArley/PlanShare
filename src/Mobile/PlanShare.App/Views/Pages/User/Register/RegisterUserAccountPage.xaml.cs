using PlanShare.App.ViewModels.Pages.User.Register;

namespace PlanShare.App.Views.Pages.User.Register;

public partial class RegisterUserAccountPage : ContentPage
{
	public RegisterUserAccountPage(RegisterUserAccountViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}