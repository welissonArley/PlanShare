using CommunityToolkit.Maui;
using PlanShare.App.Constants;
using PlanShare.App.Navigation;
using PlanShare.App.Resources.Styles.Handlers;
using PlanShare.App.ViewModels.Pages.Login.DoLogin;
using PlanShare.App.ViewModels.Pages.OnBording;
using PlanShare.App.ViewModels.Pages.User.Register;
using PlanShare.App.Views.Pages.Login.DoLogin;
using PlanShare.App.Views.Pages.User.Register;

namespace PlanShare.App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .AddPages()
			.AddNavigationService()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("Raleway-Black.ttf", FontFamily.MAIN_FONT_BLACK);
				fonts.AddFont("Raleway-Regular.ttf", FontFamily.MAIN_FONT_REGULAR);
				fonts.AddFont("Raleway-Light.ttf", FontFamily.MAIN_FONT_LIGHT);
				fonts.AddFont("WorkSans-Black.ttf", FontFamily.SECONDARY_FONT_BLACK);
				fonts.AddFont("WorkSans-Regular.ttf", FontFamily.SECONDARY_FONT_REGULAR);
			})
			.ConfigureMauiHandlers(_ =>
			{
				CustomEntryHandler.Customize();
            });

		return builder.Build();
	}

	private static MauiAppBuilder AddPages(this MauiAppBuilder appBuilder)
	{
		appBuilder.Services.AddTransient<OnBoardingViewModel>();

		appBuilder.Services.AddTransientWithShellRoute<DoLoginPage, DoLoginViewModel>(RoutePages.LOGIN_PAGE);
		appBuilder.Services.AddTransientWithShellRoute<RegisterUserAccountPage, RegisterUserAccountViewModel>(RoutePages.USER_REGISTER_ACCOUNT_PAGE);
		
		return appBuilder;
	}

	private static MauiAppBuilder AddNavigationService(this MauiAppBuilder appBuilder)
	{
		appBuilder.Services.AddSingleton<INavigationService, NavigationService>();

		return appBuilder;
	}
}