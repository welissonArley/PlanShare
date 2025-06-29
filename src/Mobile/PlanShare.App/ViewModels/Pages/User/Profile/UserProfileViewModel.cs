using CommunityToolkit.Mvvm.ComponentModel;
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

    public async Task Initialize()
    {
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
    }
}
