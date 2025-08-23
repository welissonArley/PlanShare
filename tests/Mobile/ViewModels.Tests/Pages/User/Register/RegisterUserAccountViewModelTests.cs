using CommonTestUtilities.Navigation;
using CommonTestUtilities.UseCases.User.Register;
using Moq;
using PlanShare.App.Models;
using PlanShare.App.Models.ValueObjects;
using PlanShare.App.Navigation;
using PlanShare.App.ViewModels.Pages.User.Register;
using Shouldly;

namespace ViewModels.Tests.Pages.User.Register;
public class RegisterUserAccountViewModelTests
{
    [Fact]
    public async Task Success_GoToLogin()
    {
        var (viewModel, navigationService) = CreateViewModel(Result.Success());

        var act = async () => await viewModel.GoToLoginCommand.ExecuteAsync(null);

        await act.ShouldNotThrowAsync();

        navigationService.Verify(service => service.GoToAsync(GetValidationForRoutePage($"../{RoutePages.LOGIN_PAGE}")), Times.Once);
    }

    [Fact]
    public async Task RegisterAccount_Executed_With_Valid_Result()
    {
        var (viewModel, navigationService) = CreateViewModel(Result.Success());

        var act = async () => await viewModel.RegisterAccountCommand.ExecuteAsync(null);

        await act.ShouldNotThrowAsync();

        viewModel.StatusPage.ShouldBe(StatusPage.Default);

        navigationService.Verify(service => service.GoToDashboardPage(), Times.Once);
    }

    [Fact]
    public async Task RegisterAccount_Executed_With_Invalid_Result()
    {
        var (viewModel, navigationService) = CreateViewModel(Result.Failure(["Error 1"]));

        var act = async () => await viewModel.RegisterAccountCommand.ExecuteAsync(null);

        await act.ShouldNotThrowAsync();

        viewModel.StatusPage.ShouldBe(StatusPage.Default);
        navigationService.Verify(service =>
            service.GoToAsync(
                GetValidationForRoutePage(RoutePages.ERROR_PAGE),
                GetValidationForDictionaryErrors("Error 1")
            ), Times.Once);
    }

    private ShellNavigationState GetValidationForRoutePage(string route)
    {
        return It.Is<ShellNavigationState>(state => state.Location.OriginalString.Equals(route));
    }

    private Dictionary<string, object> GetValidationForDictionaryErrors(string errorMessage)
    {
        return It.Is<Dictionary<string, object>>(dictionary =>
            dictionary.ContainsKey("errors")
            && dictionary["errors"] is IList<string>
            && ((IList<string>)dictionary["errors"]).Count == 1
            && ((IList<string>)dictionary["errors"]).Contains(errorMessage));
    }

    private (RegisterUserAccountViewModel viewModel, Mock<INavigationService> navigationService) CreateViewModel(Result result)
    {
        var navigationService = NavigationServiceBuilder.Build();
        var useCase = RegisterUserUseCaseBuilder.Build(result);

        var viewModel = new RegisterUserAccountViewModel(navigationService.Object, useCase);

        return (viewModel, navigationService);
    }
}
