using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanShare.App.Navigation;
using PlanShare.App.UseCases.User.Profile;
using PlanShare.App.UseCases.User.Update;

namespace PlanShare.App.ViewModels.Pages.User.Profile;

public partial class UserProfileViewModel : ViewModelBase
{
    [ObservableProperty]
    public Models.User model;

    private readonly IGetUserProfileUseCase _getUserProfileUseCase;
    private readonly IUpdateUserUseCase _updateUserUseCase;

    public UserProfileViewModel(
        INavigationService navigationService,
        IGetUserProfileUseCase getUserProfileUseCase,
        IUpdateUserUseCase updateUserUseCase) : base(navigationService)
    {
        _getUserProfileUseCase = getUserProfileUseCase;
        _updateUserUseCase = updateUserUseCase;
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
        {

        }
        else
            await GoToPageWithErrors(result);

        StatusPage = Models.StatusPage.Default;
    }
}
