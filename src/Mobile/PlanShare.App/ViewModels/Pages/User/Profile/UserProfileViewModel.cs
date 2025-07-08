using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanShare.App.Navigation;
using PlanShare.App.UseCases.User.Profile;

namespace PlanShare.App.ViewModels.Pages.User.Profile;

public partial class UserProfileViewModel : ViewModelBase
{
    [ObservableProperty]
    public Models.User model;

    private readonly IGetUserProfileUseCase _getUserProfileUseCase;

    public UserProfileViewModel(
        INavigationService navigationService,
        IGetUserProfileUseCase getUserProfileUseCase) : base(navigationService)
    {
        _getUserProfileUseCase = getUserProfileUseCase;
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
}
