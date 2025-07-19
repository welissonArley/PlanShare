using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.Input;
using PlanShare.App.Models.Enums;

namespace PlanShare.App.ViewModels.Popups.Files;

public partial class OptionsForProfilePhotoViewModel : ViewModelBaseForPopups
{
    private readonly IPopupService _popupService;

    public OptionsForProfilePhotoViewModel(IPopupService popupService)
    {
        _popupService = popupService;
    }

    [RelayCommand]
    public async Task OptionSelected(ChooseFileOption option)
    {
        await _popupService.ClosePopupAsync(Shell.Current, option);
    }

    [RelayCommand]
    public async Task Cancel() => await _popupService.ClosePopupAsync(Shell.Current, ChooseFileOption.None);
}
