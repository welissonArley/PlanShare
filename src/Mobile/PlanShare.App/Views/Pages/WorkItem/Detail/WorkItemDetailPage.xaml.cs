using PlanShare.App.ViewModels.Pages.WorkItem.Detail;

namespace PlanShare.App.Views.Pages.WorkItem.Detail;

public partial class WorkItemDetailPage : ContentPage
{
    public WorkItemDetailPage(WorkItemDetailViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}
