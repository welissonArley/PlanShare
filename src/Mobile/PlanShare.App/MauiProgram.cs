using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using PlanShare.App.Constants;
using PlanShare.App.Data.Network;
using PlanShare.App.Data.Network.Api;
using PlanShare.App.Data.Network.Hubs;
using PlanShare.App.Data.Storage.Preferences.User;
using PlanShare.App.Data.Storage.SecureStorage.Tokens;
using PlanShare.App.Navigation;
using PlanShare.App.Resources.Styles.Handlers;
using PlanShare.App.UseCases.Authentication.Refresh;
using PlanShare.App.UseCases.Login.DoLogin;
using PlanShare.App.UseCases.User.ChangePassword;
using PlanShare.App.UseCases.User.Photo;
using PlanShare.App.UseCases.User.Profile;
using PlanShare.App.UseCases.User.Register;
using PlanShare.App.UseCases.User.Update;
using PlanShare.App.ViewModels.Pages.Dashboard;
using PlanShare.App.ViewModels.Pages.Errors;
using PlanShare.App.ViewModels.Pages.Login.DoLogin;
using PlanShare.App.ViewModels.Pages.OnBording;
using PlanShare.App.ViewModels.Pages.User.ChangePassword;
using PlanShare.App.ViewModels.Pages.User.Connection;
using PlanShare.App.ViewModels.Pages.User.Profile;
using PlanShare.App.ViewModels.Pages.User.Register;
using PlanShare.App.ViewModels.Popups.Connection;
using PlanShare.App.ViewModels.Popups.Files;
using PlanShare.App.Views.Pages.Errors;
using PlanShare.App.Views.Pages.Login.DoLogin;
using PlanShare.App.Views.Pages.User.ChangePassword;
using PlanShare.App.Views.Pages.User.Connection;
using PlanShare.App.Views.Pages.User.Profile;
using PlanShare.App.Views.Pages.User.Register;
using PlanShare.App.Views.Popups.Connection;
using PlanShare.App.Views.Popups.Files;
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
            .AddPopups()
			.AddNavigationService()
			.AddAppSettings()
			.AddHttpClients()
			.AddUseCases()
			.AddStorage()
			.AddDeviceInfo()
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
		appBuilder.Services.AddTransient<DashboardViewModel>();

		appBuilder.Services.AddTransientWithShellRoute<ErrorsPage, ErrorsViewModel>(RoutePages.ERROR_PAGE);

		appBuilder.Services.AddTransientWithShellRoute<DoLoginPage, DoLoginViewModel>(RoutePages.LOGIN_PAGE);
		appBuilder.Services.AddTransientWithShellRoute<RegisterUserAccountPage, RegisterUserAccountViewModel>(RoutePages.USER_REGISTER_ACCOUNT_PAGE);
		appBuilder.Services.AddTransientWithShellRoute<UserProfilePage, UserProfileViewModel>(RoutePages.USER_UPDATE_PROFILE_PAGE);
		appBuilder.Services.AddTransientWithShellRoute<ChangeUserPasswordPage, ChangeUserPasswordViewModel>(RoutePages.USER_CHANGE_PASSWORD_PAGE);
		
		appBuilder.Services.AddTransientWithShellRoute<UserConnectionGeneratorPage, UserConnectionGeneratorViewModel>(RoutePages.USER_CONNECTION_GENERATOR_PAGE);
		appBuilder.Services.AddTransientWithShellRoute<UserCodeConnectionPage, UserCodeConnectionViewModel>(RoutePages.USER_CODE_CONNECTION_PAGE);

        return appBuilder;
	}

    private static MauiAppBuilder AddPopups(this MauiAppBuilder appBuilder)
    {
        appBuilder.Services.AddTransientPopup<OptionsForProfilePhotoPopup, OptionsForProfilePhotoViewModel>();
        appBuilder.Services.AddTransientPopup<OptionsForConnectionByCodePopup, OptionsForConnectionByCodeViewModel>();

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
		appBuilder.Services.AddTransient<PlanShareHandler>();

		var apiUrl = appBuilder.Configuration.GetValue<string>("ApiUrl")!;

        appBuilder.Services.AddRefitClient<IUserApi>()
			.ConfigureHttpClient(c => c.BaseAddress = new Uri(apiUrl))
			.AddHttpMessageHandler<PlanShareHandler>();

		appBuilder.Services.AddRefitClient<ILoginApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiUrl))
            .AddHttpMessageHandler<PlanShareHandler>();

		appBuilder.Services.AddRefitClient<IAuthenticationApi>()
			.ConfigureHttpClient(c => c.BaseAddress = new Uri(apiUrl));

		appBuilder.Services.AddTransient<IUserConnectionByCodeClient>(config =>
		{
			return new UserConnectionByCodeClient(apiUrl, config.GetRequiredService<ITokensStorage>());
        });

        return appBuilder;
    }

    private static MauiAppBuilder AddUseCases(this MauiAppBuilder appBuilder)
    {
		appBuilder.Services.AddTransient<IRegisterUserUseCase, RegisterUserUseCase>();
		appBuilder.Services.AddTransient<IDoLoginUseCase, DoLoginUseCase>();
		appBuilder.Services.AddTransient<IGetUserProfileUseCase, GetUserProfileUseCase>();
		appBuilder.Services.AddTransient<IUpdateUserUseCase, UpdateUserUseCase>();
		appBuilder.Services.AddTransient<IChangeUserPasswordUseCase, ChangeUserPasswordUseCase>();
		appBuilder.Services.AddTransient<IChangeUserPhotoUseCase, ChangeUserPhotoUseCase>();
		appBuilder.Services.AddTransient<IUseRefreshTokenUseCase, UseRefreshTokenUseCase>();

        return appBuilder;
    }

    private static MauiAppBuilder AddStorage(this MauiAppBuilder appBuilder)
    {
        appBuilder.Services.AddSingleton<IUserStorage, UserStorage>();

		if(DeviceInfo.Platform == DevicePlatform.iOS && DeviceInfo.DeviceType == DeviceType.Virtual)
            appBuilder.Services.AddSingleton<ITokensStorage, TokensStorageForVirtualDevice>();
        else
            appBuilder.Services.AddSingleton<ITokensStorage, TokensStorage>();

        return appBuilder;
    }

	private static MauiAppBuilder AddDeviceInfo(this MauiAppBuilder appBuilder)
	{
		appBuilder.Services.AddSingleton(DeviceDisplay.Current);
		appBuilder.Services.AddSingleton(MediaPicker.Default);

		return appBuilder;
    }
}