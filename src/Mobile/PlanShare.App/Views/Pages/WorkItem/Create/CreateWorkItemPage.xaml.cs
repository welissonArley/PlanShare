using PlanShare.App.ViewModels.Pages.WorkItem.Create;

namespace PlanShare.App.Views.Pages.WorkItem.Create;

public partial class CreateWorkItemPage : ContentPage
{
    public CreateWorkItemPage(CreateWorkItemViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}
