﻿using PlanShare.App.ViewModels.Popups;

namespace PlanShare.App.Navigation;

public interface INavigationService
{
    Task GoToAsync(ShellNavigationState state);
    Task GoToAsync(ShellNavigationState route, Dictionary<string, object> parameters);
    Task ClosePage();
    Task GoToDashboardPage();
    Task ShowSuccessFeedback(string message);
    Task<TResult> ShowPopup<TViewModel, TResult>()
        where TViewModel : ViewModelBaseForPopups
        where TResult : notnull;
}
