using PlanShare.App.Constants;
using PlanShare.App.Navigation;
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
			.AddPages()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("Raleway-Black.ttf", FontFamily.MAIN_FONT_BLACK);
				fonts.AddFont("Raleway-Regular.ttf", FontFamily.MAIN_FONT_REGULAR);
				fonts.AddFont("Raleway-Thin.ttf", FontFamily.MAIN_FONT_THIN);
				fonts.AddFont("WorkSans-Black.ttf", FontFamily.SECONDARY_FONT_BLACK);
				fonts.AddFont("WorkSans-Regular.ttf", FontFamily.SECONDARY_FONT_REGULAR);
			});

		return builder.Build();
	}

	private static MauiAppBuilder AddPages(this MauiAppBuilder appBuilder)
	{
		Routing.RegisterRoute(RoutePages.LOGIN_PAGE, typeof(DoLoginPage));
		Routing.RegisterRoute(RoutePages.USER_REGISTER_ACCOUNT_PAGE, typeof(RegisterUserAccountPage));

		return appBuilder;
	}
}
