using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanShare.App.Navigation;
using PlanShare.App.Resources;
using PlanShare.App.UseCases.WorkItem.Delete;
using PlanShare.App.UseCases.WorkItem.GetById;

namespace PlanShare.App.ViewModels.Pages.WorkItem.Detail;

public partial class WorkItemDetailViewModel : ViewModelBase, IQueryAttributable
{
    [ObservableProperty]
    public Models.WorkItemDetail model;

    private Guid _workItemId;
    private readonly IGetWorkItemByIdUseCase _getWorkItemByIdUseCase;
    private readonly IDeleteWorkItemUseCase _deleteWorkItemUseCase;

    public WorkItemDetailViewModel(INavigationService navigationService, IGetWorkItemByIdUseCase getWorkItemByIdUseCase, IDeleteWorkItemUseCase deleteWorkItemUseCase) : base(navigationService)
    {
        _getWorkItemByIdUseCase = getWorkItemByIdUseCase;
        _deleteWorkItemUseCase = deleteWorkItemUseCase;
    }

    [RelayCommand]
    public async Task Initialize()
    {
        StatusPage = Models.StatusPage.Loading;

        var result = await _getWorkItemByIdUseCase.Execute(_workItemId);
        if (result.IsSuccess)
            Model = result.Response!;
        else
            await GoToPageWithErrors(result);

        StatusPage = Models.StatusPage.Default;
    }

    [RelayCommand]
    public async Task Delete()
    {
        var confirmed = await _navigationService.ShowConfirmation(
            ResourceTexts.DELETE_TASK,
            ResourceTexts.DELETE_TASK_CONFIRMATION,
            ResourceTexts.DELETE_TASK,
            ResourceTexts.TITLE_CANCEL);

        if (confirmed == false)
            return;

        StatusPage = Models.StatusPage.Sending;

        var result = await _deleteWorkItemUseCase.Execute(_workItemId);
        if (result.IsSuccess)
        {
            await _navigationService.ShowSuccessFeedback(ResourceTexts.WORK_ITEM_SUCCESSFULLY_DELETED);
            await _navigationService.ClosePage();
        }
        else
            await GoToPageWithErrors(result);

        StatusPage = Models.StatusPage.Default;
    }

    [RelayCommand]
    public async Task EditWorkItem()
    {
        var parameters = new Dictionary<string, object>
        {
            { "id", _workItemId },
            { "workItem", Model }
        };

        await _navigationService.GoToAsync(RoutePages.EDIT_WORK_ITEM_PAGE, parameters);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("id", out var value) && value is Guid id)
            _workItemId = id;
    }
}
