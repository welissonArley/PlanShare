using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Controls.Shapes;
using PlanShare.App.Constants;
using PlanShare.App.Extensions;
using PlanShare.App.Resources;
using PlanShare.App.ViewModels.Popups;

namespace PlanShare.App.Navigation;

public class NavigationService : INavigationService
{
    private readonly IPopupService _popupService;

    public NavigationService(IPopupService popupService)
    {
        _popupService = popupService;
    }

    public async Task GoToAsync(ShellNavigationState state) => await Shell.Current.GoToAsync(state);
    public async Task GoToAsync(ShellNavigationState route, Dictionary<string, object> parameters)
        => await Shell.Current.GoToAsync(route, parameters);

    public async Task ClosePage() => await GoToAsync("..");
    public async Task GoToDashboardPage() => await GoToAsync($"//{RoutePages.DASHBOARD_PAGE}");
    public async Task GoToOnboardingPage() => await GoToAsync($"//{RoutePages.ONBOARDING_PAGE}");

    public async Task ShowSuccessFeedback(string message)
    {
        var snackBarOptions = new SnackbarOptions
        {
            BackgroundColor = Application.Current!.GetHighlightColor(),
            TextColor = Application.Current!.GetSecondaryColor(),
            CornerRadius = new CornerRadius(10),
            ActionButtonTextColor = Application.Current!.GetSecondaryColor(),
            Font = Microsoft.Maui.Font.OfSize(FontFamily.MAIN_FONT_BLACK, 14),
            ActionButtonFont = Microsoft.Maui.Font.OfSize(FontFamily.SECONDARY_FONT_REGULAR, 14),
            CharacterSpacing = 0.1
        };

        var duration = TimeSpan.FromSeconds(3);

        var snackBar = Snackbar.Make(message,
            action: null,
            actionButtonText: ResourceTexts.TITLE_CLOSE,
            duration,
            snackBarOptions);

        await snackBar.Show();
    }

    public async Task ShowFailureFeedback(string message)
    {
        var snackBarOptions = new SnackbarOptions
        {
            BackgroundColor = Application.Current!.GetDangerColor(),
            TextColor = Application.Current!.GetSecondaryColor(),
            CornerRadius = new CornerRadius(10),
            ActionButtonTextColor = Application.Current!.GetSecondaryColor(),
            Font = Microsoft.Maui.Font.OfSize(FontFamily.MAIN_FONT_BLACK, 14),
            ActionButtonFont = Microsoft.Maui.Font.OfSize(FontFamily.SECONDARY_FONT_REGULAR, 14),
            CharacterSpacing = 0.1
        };

        var duration = TimeSpan.FromSeconds(4);

        var snackBar = Snackbar.Make(message,
            action: null,
            actionButtonText: ResourceTexts.TITLE_CLOSE,
            duration,
            snackBarOptions);

        await snackBar.Show();
    }

    public async Task<TResult> ShowPopup<TViewModel, TResult>()
        where TViewModel : ViewModelBaseForPopups
        where TResult : notnull
    {
        var popupOptions = new PopupOptions
        {
            CanBeDismissedByTappingOutsideOfPopup = false,
            Shadow = null,
            Shape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(10),
                StrokeThickness = 0
            }
        };

        var result = await _popupService.ShowPopupAsync<TViewModel, TResult>(Shell.Current, popupOptions);

        return result.Result!;
    }
}
