using CommonTestUtilities.Navigation;
using Moq;
using PlanShare.App.Navigation;
using PlanShare.App.ViewModels.Pages.OnBording;
using Shouldly;

namespace ViewModels.Tests.Pages.OnBording;
public class OnBoardingViewModelTests
{
    [Fact]
    public async Task Success_LoginWithEmailAndPassword()
    {
        var (viewModel, navigationService) = CreateViewModel();

        var act = async () => await viewModel.LoginWithEmailAndPasswordCommand.ExecuteAsync(null);

        await act.ShouldNotThrowAsync();

        navigationService.Verify(service =>
            service.GoToAsync(
                It.Is<ShellNavigationState>(state =>
                    state.Location.OriginalString.Equals(RoutePages.LOGIN_PAGE))), Times.Once);
    }

    [Fact]
    public async Task Success_RegisterUserAccount()
    {
        var (viewModel, navigationService) = CreateViewModel();

        var act = async () => await viewModel.RegisterUserAccountCommand.ExecuteAsync(null);

        await act.ShouldNotThrowAsync();

        navigationService.Verify(service =>
            service.GoToAsync(
                It.Is<ShellNavigationState>(state =>
                    state.Location.OriginalString.Equals(RoutePages.USER_REGISTER_ACCOUNT_PAGE))), Times.Once);
    }

    private (OnBoardingViewModel viewModel, Mock<INavigationService> navigationService) CreateViewModel()
    {
        var navigationService = NavigationServiceBuilder.Build();

        var viewModel = new OnBoardingViewModel(navigationService.Object);

        return (viewModel, navigationService);
    }
}
