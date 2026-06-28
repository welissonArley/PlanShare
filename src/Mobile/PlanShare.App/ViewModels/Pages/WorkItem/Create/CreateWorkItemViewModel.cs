using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanShare.App.Data.Storage.Preferences.User;
using PlanShare.App.Models;
using PlanShare.App.Navigation;
using PlanShare.App.Resources;
using PlanShare.App.UseCases.WorkItem.Register;

namespace PlanShare.App.ViewModels.Pages.WorkItem.Create;

public partial class CreateWorkItemViewModel : ViewModelBase, IQueryAttributable
{
    [ObservableProperty]
    public Models.WorkItem model;

    private readonly IRegisterWorkItemUseCase _registerWorkItemUseCase;
    private readonly IUserStorage _userStorage;

    public CreateWorkItemViewModel(INavigationService navigationService, IRegisterWorkItemUseCase registerWorkItemUseCase, IUserStorage userStorage) : base(navigationService)
    {
        _registerWorkItemUseCase = registerWorkItemUseCase;
        _userStorage = userStorage;

        Model = new Models.WorkItem();
    }

    [RelayCommand]
    public async Task Save()
    {
        StatusPage = Models.StatusPage.Sending;

        var result = await _registerWorkItemUseCase.Execute(Model);
        if (result.IsSuccess)
        {
            await _navigationService.ShowSuccessFeedback(ResourceTexts.WORK_ITEM_SUCCESSFULLY_CREATED);
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
        Model.Assignees.Clear();

        var loggedUser = _userStorage.Get();
        Model.Assignees.Add(new AssigneeSelection { Id = loggedUser.Id, Name = loggedUser.Name });

        if (query.TryGetValue("connectedUsers", out var value) && value is List<ConnectedUser> connectedUsers)
        {
            foreach (var user in connectedUsers)
                Model.Assignees.Add(new AssigneeSelection { Id = user.Id, Name = user.Name });
        }
    }
}
