using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanShare.App.Navigation;
using System.Collections.ObjectModel;

namespace PlanShare.App.ViewModels.Pages.Errors;
public partial class ErrorsViewModel : ObservableObject, IQueryAttributable
{
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    public ObservableCollection<string> errorsList;

    public ErrorsViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    public async Task Close() => await _navigationService.GoToAsync("..");

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if(query.Count > 0)
        {
            var errorsList = query["errors"] as List<string>;

            if (errorsList is not null)
                ErrorsList = new ObservableCollection<string>(errorsList);
        }
    }
}
