using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanShare.App.Navigation;
using System.Collections.ObjectModel;

namespace PlanShare.App.ViewModels.Pages.Errors;
public partial class ErrorsViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    public ObservableCollection<string> errorsList;

    public ErrorsViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        ErrorsList = new ObservableCollection<string>()
        {
            "Error 1: Invalid input",
            "Error 2: Network connection lost",
            "Error 3: File not found"
        };
    }

    [RelayCommand]
    public async Task Close() => await _navigationService.GoToAsync("..");
}
