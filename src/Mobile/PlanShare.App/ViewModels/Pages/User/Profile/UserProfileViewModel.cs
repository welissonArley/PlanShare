using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Shapes;
using PlanShare.App.Navigation;
using PlanShare.App.Resources;
using PlanShare.App.UseCases.User.Profile;
using PlanShare.App.UseCases.User.Update;
using PlanShare.App.ViewModels.Popups.Files;

namespace PlanShare.App.ViewModels.Pages.User.Profile;

public partial class UserProfileViewModel : ViewModelBase
{
    [ObservableProperty]
    public Models.User model;

    private readonly IGetUserProfileUseCase _getUserProfileUseCase;
    private readonly IUpdateUserUseCase _updateUserUseCase;

    private readonly IPopupService _popupService;

    public UserProfileViewModel(
        INavigationService navigationService,
        IGetUserProfileUseCase getUserProfileUseCase,
        IUpdateUserUseCase updateUserUseCase,
        IPopupService popupService) : base(navigationService)
    {
        _getUserProfileUseCase = getUserProfileUseCase;
        _updateUserUseCase = updateUserUseCase;

        _popupService = popupService;
    }

    [RelayCommand]
    public async Task Initialize()
    {
        StatusPage = Models.StatusPage.Loading;

        var result = await _getUserProfileUseCase.Execute();
        if (result.IsSuccess)
            Model = result.Response!;
        else
            await GoToPageWithErrors(result);

        StatusPage = Models.StatusPage.Default;
    }

    [RelayCommand]
    public async Task UpdateProfile()
    {
        StatusPage = Models.StatusPage.Sending;

        var result = await _updateUserUseCase.Execute(Model);
        if (result.IsSuccess)
            await _navigationService.ShowSuccessFeedback(ResourceTexts.PROFILE_INFORMATION_SUCCESSFULLY_UPDATED);
        else
            await GoToPageWithErrors(result);

        StatusPage = Models.StatusPage.Default;
    }

    [RelayCommand]
    public async Task ChangePassword() => await _navigationService.GoToAsync(RoutePages.USER_CHANGE_PASSWORD_PAGE);

    [RelayCommand]
    public async Task ChangeProfilePhoto()
    {
        var popupOptions = new PopupOptions
        {
            Shadow = null,
            Shape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(10),
                StrokeThickness = 0
            }
        };

        await _popupService.ShowPopupAsync<OptionsForProfilePhotoViewModel>(Shell.Current, popupOptions);
    }
}
