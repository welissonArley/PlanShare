using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanShare.App.Models;
using PlanShare.App.Navigation;
using PlanShare.App.Resources;
using PlanShare.App.UseCases.WorkItem.Update;

namespace PlanShare.App.ViewModels.Pages.WorkItem.Edit;

public partial class EditWorkItemViewModel : ViewModelBase, IQueryAttributable
{
    [ObservableProperty]
    public Models.WorkItem model;

    private Guid _workItemId;
    private readonly IUpdateWorkItemUseCase _updateWorkItemUseCase;

    public EditWorkItemViewModel(INavigationService navigationService, IUpdateWorkItemUseCase updateWorkItemUseCase) : base(navigationService)
    {
        _updateWorkItemUseCase = updateWorkItemUseCase;
    }

    [RelayCommand]
    public async Task Save()
    {
        StatusPage = Models.StatusPage.Sending;

        var result = await _updateWorkItemUseCase.Execute(_workItemId, Model);
        if (result.IsSuccess)
        {
            await _navigationService.ShowSuccessFeedback(ResourceTexts.WORK_ITEM_SUCCESSFULLY_UPDATED);
            await _navigationService.ClosePage();
        }
        else
            await GoToPageWithErrors(result);

        StatusPage = Models.StatusPage.Default;
    }

    [RelayCommand]
    public async Task GoBack() => await _navigationService.ClosePage();

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("id", out var idValue) && idValue is Guid id)
            _workItemId = id;

        if (query.TryGetValue("workItem", out var value) && value is WorkItemDetail workItem)
        {
            Model = new Models.WorkItem
            {
                Title = workItem.Title,
                Description = workItem.Description,
                DueDate = workItem.DueDate
            };

            Model.Assignees.Clear();

            foreach (var assignee in workItem.Assignees)
                Model.Assignees.Add(new AssigneeSelection { Id = assignee.Id, Name = assignee.Name });
        }
    }
}
