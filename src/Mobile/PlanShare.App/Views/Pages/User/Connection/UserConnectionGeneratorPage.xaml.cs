using PlanShare.App.ViewModels.Pages.User.Connection;

namespace PlanShare.App.Views.Pages.User.Connection;

public partial class UserConnectionGeneratorPage : ContentPage
{
	public UserConnectionGeneratorPage(UserConnectionGeneratorViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
    }
}