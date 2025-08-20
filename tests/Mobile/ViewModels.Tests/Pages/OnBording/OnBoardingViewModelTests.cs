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

        navigationService.Verify(service => service.GoToAsync(RoutePages.LOGIN_PAGE), Times.Once);
    }

    private (OnBoardingViewModel viewModel, Mock<INavigationService> navigationService) CreateViewModel()
    {
        var navigationService = NavigationServiceBuilder.Build();

        var viewModel = new OnBoardingViewModel(navigationService.Object);

        return (viewModel, navigationService);
    }
}
