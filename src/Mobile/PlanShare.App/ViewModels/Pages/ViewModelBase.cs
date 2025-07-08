using CommunityToolkit.Mvvm.ComponentModel;
using PlanShare.App.Models;
using PlanShare.App.Models.ValueObjects;
using PlanShare.App.Navigation;

namespace PlanShare.App.ViewModels.Pages;

public abstract partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    public StatusPage statusPage;

    protected readonly INavigationService _navigationService;

    protected ViewModelBase(INavigationService navigationService)
    {
        StatusPage = StatusPage.Default;
        _navigationService = navigationService;
    }

    protected async Task GoToPageWithErrors(Result result)
    {
        var parameters = new Dictionary<string, object>
        {
            { "errors", result.ErrorMessages! }
        };

        await _navigationService.GoToAsync(RoutePages.ERROR_PAGE, parameters);
    }
}
