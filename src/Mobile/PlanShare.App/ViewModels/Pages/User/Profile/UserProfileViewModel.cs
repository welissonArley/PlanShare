using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlanShare.App.Navigation;
using PlanShare.App.UseCases.User.Profile;

namespace PlanShare.App.ViewModels.Pages.User.Profile;

public partial class UserProfileViewModel : ViewModelBase
{
    [ObservableProperty]
    public Models.User model;

    private readonly INavigationService _navigationService;
    private readonly IGetUserProfileUseCase _getUserProfileUseCase;

    public UserProfileViewModel(INavigationService navigationService, IGetUserProfileUseCase getUserProfileUseCase)
    {
        _navigationService = navigationService;
        _getUserProfileUseCase = getUserProfileUseCase;
    }

    [RelayCommand]
    public async Task Initialize()
    {
        StatusPage = Models.StatusPage.Loading;

        var result = await _getUserProfileUseCase.Execute();
        if (result.IsSuccess == false)
        {
            var parameters = new Dictionary<string, object>
            {
                { "errors", result.ErrorMessages! }
            };

            await _navigationService.GoToAsync(RoutePages.ERROR_PAGE, parameters);
        }
        else
            Model = result.Response!;

        StatusPage = Models.StatusPage.Default;
    }
}
