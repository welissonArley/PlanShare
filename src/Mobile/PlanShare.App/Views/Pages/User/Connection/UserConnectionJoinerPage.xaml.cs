using PlanShare.App.ViewModels.Pages.User.Connection;

namespace PlanShare.App.Views.Pages.User.Connection;

public partial class UserConnectionJoinerPage : ContentPage
{
	public UserConnectionJoinerPage(UserConnectionJoinerViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}