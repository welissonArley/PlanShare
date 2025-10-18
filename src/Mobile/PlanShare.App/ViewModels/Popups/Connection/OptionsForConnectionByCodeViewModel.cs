using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.Input;
using PlanShare.App.Models.Enums;

namespace PlanShare.App.ViewModels.Popups.Connection;
public partial class OptionsForConnectionByCodeViewModel : ViewModelBaseForPopups
{
    private readonly IPopupService _popupService;

    public OptionsForConnectionByCodeViewModel(IPopupService popupService)
    {
        _popupService = popupService;
    }

    [RelayCommand]
    public async Task OptionSelected(ChooseCodeConnectionOption option) => await _popupService.ClosePopupAsync(Shell.Current, option);
}
