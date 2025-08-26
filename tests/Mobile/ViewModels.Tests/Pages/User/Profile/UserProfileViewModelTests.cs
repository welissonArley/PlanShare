using CommonTestUtilities.Models;
using CommonTestUtilities.Navigation;
using CommonTestUtilities.UseCases.User.Profile;
using CommonTestUtilities.UseCases.User.Update;
using Moq;
using PlanShare.App.Models;
using PlanShare.App.Models.ValueObjects;
using PlanShare.App.Navigation;
using PlanShare.App.Resources;
using PlanShare.App.ViewModels.Pages.User.Profile;
using Shouldly;
using ViewModels.Tests.Extensions;

namespace ViewModels.Tests.Pages.User.Profile;
public class UserProfileViewModelTests
{
    [Fact]
    public async Task Success_ChangePassword()
    {
        var (viewModel, navigationService) = CreateViewModel(Result<PlanShare.App.Models.User>.Success(UserBuilder.Build()));

        var act = async () => await viewModel.ChangePasswordCommand.ExecuteAsync(null);

        await act.ShouldNotThrowAsync();

        navigationService.VerifyGoTo(RoutePages.USER_CHANGE_PASSWORD_PAGE, Times.Once);
    }

    [Fact]
    public async Task UpdateProfile_Executed_With_Valid_Result()
    {
        var (viewModel, navigationService) = CreateViewModel(Result<PlanShare.App.Models.User>.Success(UserBuilder.Build()));

        var act = async () => await viewModel.UpdateProfileCommand.ExecuteAsync(null);

        await act.ShouldNotThrowAsync();

        viewModel.StatusPage.ShouldBe(StatusPage.Default);

        navigationService.Verify(service => service.ShowSuccessFeedback(ResourceTexts.PROFILE_INFORMATION_SUCCESSFULLY_UPDATED), Times.Once);
    }

    [Fact]
    public async Task UpdateProfile_Executed_With_Invalid_Result()
    {
        var (viewModel, navigationService) = CreateViewModel(Result<PlanShare.App.Models.User>.Failure(["Error 1"]));

        var act = async () => await viewModel.UpdateProfileCommand.ExecuteAsync(null);

        await act.ShouldNotThrowAsync();

        viewModel.StatusPage.ShouldBe(StatusPage.Default);

        navigationService.VerifyGoTo(RoutePages.ERROR_PAGE, ["Error 1"], Times.Once);
    }

    [Fact]
    public async Task Initialize_Executed_With_Valid_Result()
    {
        var user = UserBuilder.Build();

        var (viewModel, navigationService) = CreateViewModel(Result<PlanShare.App.Models.User>.Success(user));

        var act = async () => await viewModel.InitializeCommand.ExecuteAsync(null);

        await act.ShouldNotThrowAsync();

        viewModel.StatusPage.ShouldBe(StatusPage.Default);

        viewModel.Model.ShouldNotBeNull();
        viewModel.Model.Name.ShouldBe(user.Name);
        viewModel.Model.Email.ShouldBe(user.Email);
    }

    [Fact]
    public async Task Initialize_Executed_With_Invalid_Result()
    {
        var (viewModel, navigationService) = CreateViewModel(Result<PlanShare.App.Models.User>.Failure(["Error 1"]));

        var act = async () => await viewModel.InitializeCommand.ExecuteAsync(null);

        await act.ShouldNotThrowAsync();

        viewModel.StatusPage.ShouldBe(StatusPage.Default);

        navigationService.VerifyGoTo(RoutePages.ERROR_PAGE, ["Error 1"], Times.Once);
    }

    private (UserProfileViewModel viewModel, Mock<INavigationService> navigationService) CreateViewModel(Result<PlanShare.App.Models.User> result)
    {
        var navigationService = NavigationServiceBuilder.Build();
        var updateUserUseCase = UpdateUserUseCaseBuilder.Build(result);
        var getProfileUseCase = GetUserProfileUseCaseBuilder.Build(result);

        var viewModel = new UserProfileViewModel(
            navigationService.Object,
            getProfileUseCase,
            updateUserUseCase,
            null,
            null);

        return (viewModel, navigationService);
    }
}
