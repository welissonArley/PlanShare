using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanShare.App.Navigation;

namespace PlanShare.App.ViewModels.Pages.Errors;
public partial class ErrorsViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    public ErrorsViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    public async Task Close() => await _navigationService.GoToAsync("..");
}
