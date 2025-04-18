using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using PlanShare.App.Constants;
using PlanShare.App.Data.Network.Api;
using PlanShare.App.Navigation;
using PlanShare.App.Resources.Styles.Handlers;
using PlanShare.App.UseCases.User.Register;
using PlanShare.App.ViewModels.Pages.Login.DoLogin;
using PlanShare.App.ViewModels.Pages.OnBording;
using PlanShare.App.ViewModels.Pages.User.Register;
using PlanShare.App.Views.Pages.Login.DoLogin;
using PlanShare.App.Views.Pages.User.Register;
using Refit;
using SkiaSharp.Views.Maui.Controls.Hosting;
using System.Reflection;

namespace PlanShare.App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
			.UseSkiaSharp()
            .AddPages()
			.AddNavigationService()
			.AddAppSettings()
			.AddHttpClients()
			.AddUseCases()
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

	private static MauiAppBuilder AddAppSettings(this MauiAppBuilder appBuilder)
    {
		using var fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PlanShare.App.appsettings.json");

		var config = new ConfigurationBuilder().AddJsonStream(fileStream!).Build();

		appBuilder.Configuration.AddConfiguration(config);

        return appBuilder;
    }

    private static MauiAppBuilder AddHttpClients(this MauiAppBuilder appBuilder)
    {
		var apiUrl = appBuilder.Configuration.GetValue<string>("ApiUrl")!;

        appBuilder.Services.AddRefitClient<IUserApi>()
			.ConfigureHttpClient(c => c.BaseAddress = new Uri(apiUrl));

        return appBuilder;
    }

    private static MauiAppBuilder AddUseCases(this MauiAppBuilder appBuilder)
    {
		appBuilder.Services.AddTransient<IRegisterUserUseCase, RegisterUserUseCase>();

        return appBuilder;
    }
}