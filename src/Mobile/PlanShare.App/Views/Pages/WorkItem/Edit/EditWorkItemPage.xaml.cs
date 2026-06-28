using PlanShare.App.ViewModels.Pages.WorkItem.Edit;

namespace PlanShare.App.Views.Pages.WorkItem.Edit;

public partial class EditWorkItemPage : ContentPage
{
    public EditWorkItemPage(EditWorkItemViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}
