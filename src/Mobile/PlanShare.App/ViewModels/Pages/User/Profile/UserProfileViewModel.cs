using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanShare.App.Models.Enums;
using PlanShare.App.Navigation;
using PlanShare.App.Resources;
using PlanShare.App.UseCases.User.Photo;
using PlanShare.App.UseCases.User.Profile;
using PlanShare.App.UseCases.User.Update;
using PlanShare.App.ViewModels.Popups.Files;

namespace PlanShare.App.ViewModels.Pages.User.Profile;

public partial class UserProfileViewModel : ViewModelBase
{
    [ObservableProperty]
    public Models.User model;

    [ObservableProperty]
    public string? photoPath;

    private readonly IGetUserProfileUseCase _getUserProfileUseCase;
    private readonly IUpdateUserUseCase _updateUserUseCase;
    private readonly IChangeUserPhotoUseCase _changeUserPhotoUseCase;

    private readonly IMediaPicker _mediaPicker;

    public UserProfileViewModel(
        INavigationService navigationService,
        IGetUserProfileUseCase getUserProfileUseCase,
        IUpdateUserUseCase updateUserUseCase,
        IChangeUserPhotoUseCase changeUserPhotoUseCase,
        IMediaPicker mediaPicker) : base(navigationService)
    {
        _getUserProfileUseCase = getUserProfileUseCase;
        _updateUserUseCase = updateUserUseCase;
        _changeUserPhotoUseCase = changeUserPhotoUseCase;
        _mediaPicker = mediaPicker;
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
        var optionSelected = await _navigationService.ShowPopup<OptionsForProfilePhotoViewModel, ChooseFileOption>();
        switch (optionSelected)
        {
            case ChooseFileOption.TakePicture:
                {
                    var photo = await _mediaPicker.CapturePhotoAsync();
                    await UpdateProfilePhoto(photo);
                }
                break;
            case ChooseFileOption.UploadFromGallery:
                {
                    var photo = await _mediaPicker.PickPhotoAsync();
                    await UpdateProfilePhoto(photo);
                }
                break;
            case ChooseFileOption.DeleteCurrentPicture:
                {
                    PhotoPath = null;
                }
                break;
        }
    }

    private async Task UpdateProfilePhoto(FileResult? photo)
    {
        if(photo is not null)
        {
            StatusPage = Models.StatusPage.Sending;

            var result = await _changeUserPhotoUseCase.Execute(photo);
            if (result.IsSuccess)
                await _navigationService.ShowSuccessFeedback(ResourceTexts.PROFILE_PHOTO_SUCCESSFULLY_UPDATED);
            else
                await GoToPageWithErrors(result);

            StatusPage = Models.StatusPage.Default;

            PhotoPath = photo.FullPath;
        }
    }
}
