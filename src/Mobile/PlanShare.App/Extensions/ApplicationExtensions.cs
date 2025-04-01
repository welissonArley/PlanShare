namespace PlanShare.App.Extensions;

public static class ApplicationExtensions
{
    public static bool IsLightMode(this Application application) => application.RequestedTheme == AppTheme.Light;

    public static Color GetPrimaryColor(this Application application)
    {
        var isLightMode = application.IsLightMode();

        var key = isLightMode ? "PRIMARY_COLOR_LIGHT" : "PRIMARY_COLOR_DARK";

        return (Color)application!.Resources[key];
    }

    public static Color GetLineColor(this Application application)
    {
        var isLightMode = application.IsLightMode();

        var key = isLightMode ? "LINES_COLOR_LIGHT" : "LINES_COLOR_DARK";

        return (Color)application!.Resources[key];
    }
}
