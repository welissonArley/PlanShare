using CommonTestUtilities.Navigation;
using CommonTestUtilities.UseCases.Login.DoLogin;
using Moq;
using PlanShare.App.Models;
using PlanShare.App.Models.ValueObjects;
using PlanShare.App.Navigation;
using PlanShare.App.ViewModels.Pages.Login.DoLogin;
using Shouldly;
using ViewModels.Tests.Extensions;

namespace ViewModels.Tests.Pages.Login.DoLogin;
public class DoLoginViewModelTests
{
    [Fact]
    public async Task DoLogin_Executed_With_Valid_Result()
    {
        var (viewModel, navigationService) = CreateViewModel(Result.Success());

        var act = async () => await viewModel.DoLoginCommand.ExecuteAsync(null);

        await act.ShouldNotThrowAsync();

        viewModel.StatusPage.ShouldBe(StatusPage.Default);

        navigationService.Verify(service => service.GoToDashboardPage(), Times.Once);
    }

    [Fact]
    public async Task DoLogin_Executed_With_Invalid_Result()
    {
        var (viewModel, navigationService) = CreateViewModel(Result.Failure(["Error 1"]));

        var act = async () => await viewModel.DoLoginCommand.ExecuteAsync(null);

        await act.ShouldNotThrowAsync();

        viewModel.StatusPage.ShouldBe(StatusPage.Default);
        
        navigationService.VerifyGoTo(RoutePages.ERROR_PAGE, ["Error 1"], Times.Once);
    }

    private (DoLoginViewModel viewModel, Mock<INavigationService> navigationService) CreateViewModel(Result result)
    {
        var navigationService = NavigationServiceBuilder.Build();
        var useCase = DoLoginUseCaseBuilder.Build(result);

        var viewModel = new DoLoginViewModel(navigationService.Object, useCase);

        return (viewModel, navigationService);
    }
}
