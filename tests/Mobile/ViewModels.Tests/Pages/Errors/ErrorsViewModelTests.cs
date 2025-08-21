using CommonTestUtilities.Navigation;
using Moq;
using PlanShare.App.Navigation;
using PlanShare.App.ViewModels.Pages.Errors;
using Shouldly;

namespace ViewModels.Tests.Pages.Errors;
public class ErrorsViewModelTests
{
    [Fact]
    public async Task Success_Close()
    {
        var (viewModel, navigationService) = CreateViewModel();

        var act = async () => await viewModel.CloseCommand.ExecuteAsync(null);

        await act.ShouldNotThrowAsync();

        navigationService.Verify(navigationService => navigationService.ClosePage(), Times.Once);
    }

    [Fact]
    public void Success_ApplyQueryAttributes()
    {
        var (viewModel, navigationService) = CreateViewModel();

        viewModel.ApplyQueryAttributes(new Dictionary<string, object>
        {
            { "errors", new List<string> { "Error 1", "Error 2" } }
        });

        viewModel.ErrorsList.ShouldNotBeNull();
        viewModel.ErrorsList.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(2);
            errors.ShouldContain("Error 1");
            errors.ShouldContain("Error 2");
        });
    }

    private (ErrorsViewModel viewModel, Mock<INavigationService> navigationService) CreateViewModel()
    {
        var navigationService = NavigationServiceBuilder.Build();

        var viewModel = new ErrorsViewModel(navigationService.Object);

        return (viewModel, navigationService);
    }
}
