using PlanShare.App.ViewModels.Pages.User.Profile;

namespace PlanShare.App.Views.Pages.User.Profile;

public partial class UserProfilePage : ContentPage
{
	public UserProfilePage(UserProfileViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}