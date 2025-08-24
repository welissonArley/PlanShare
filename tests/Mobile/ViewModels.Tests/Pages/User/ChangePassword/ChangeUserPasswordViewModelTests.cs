using CommonTestUtilities.Navigation;
using CommonTestUtilities.UseCases.User.ChangePassword;
using Moq;
using PlanShare.App.Models;
using PlanShare.App.Models.ValueObjects;
using PlanShare.App.Navigation;
using PlanShare.App.Resources;
using PlanShare.App.ViewModels.Pages.User.ChangePassword;
using Shouldly;
using ViewModels.Tests.Extensions;

namespace ViewModels.Tests.Pages.User.ChangePassword;
public class ChangeUserPasswordViewModelTests
{
    [Fact]
    public async Task ChangePassword_Executed_With_Valid_Result()
    {
        var (viewModel, navigationService) = CreateViewModel(Result.Success());

        var act = async () => await viewModel.ChangePasswordCommand.ExecuteAsync(null);

        await act.ShouldNotThrowAsync();

        viewModel.StatusPage.ShouldBe(StatusPage.Default);

        navigationService.Verify(service => service.ClosePage(), Times.Once);
        navigationService.Verify(service => service.ShowSuccessFeedback(ResourceTexts.PASSWORD_SUCCESSFULLY_CHANGED), Times.Once);
    }

    [Fact]
    public async Task ChangePassword_Executed_With_Invalid_Result()
    {
        var (viewModel, navigationService) = CreateViewModel(Result.Failure(["Error 1"]));

        var act = async () => await viewModel.ChangePasswordCommand.ExecuteAsync(null);

        await act.ShouldNotThrowAsync();

        viewModel.StatusPage.ShouldBe(StatusPage.Default);

        navigationService.VerifyGoTo(RoutePages.ERROR_PAGE, ["Error 1"], Times.Once);
    }

    private (ChangeUserPasswordViewModel viewModel, Mock<INavigationService> navigationService) CreateViewModel(Result result)
    {
        var navigationService = NavigationServiceBuilder.Build();
        var useCase = ChangeUserPasswordUseCaseBuild.Build(result);

        var viewModel = new ChangeUserPasswordViewModel(navigationService.Object, useCase);

        return (viewModel, navigationService);
    }
}
