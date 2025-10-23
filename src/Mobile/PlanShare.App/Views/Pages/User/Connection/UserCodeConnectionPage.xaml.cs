using PlanShare.App.ViewModels.Pages.User.Connection;

namespace PlanShare.App.Views.Pages.User.Connection;

public partial class UserCodeConnectionPage : PinCodes.Authorization.Views.Pages.CodePage
{
	public UserCodeConnectionPage(UserCodeConnectionViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}