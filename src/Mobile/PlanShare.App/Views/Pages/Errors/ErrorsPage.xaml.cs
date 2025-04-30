using PlanShare.App.ViewModels.Pages.Errors;

namespace PlanShare.App.Views.Pages.Errors;

public partial class ErrorsPage : ContentPage
{
	public ErrorsPage(ErrorsViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}