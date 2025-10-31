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

    public static Color GetSecondaryColor(this Application application)
    {
        var isLightMode = application.IsLightMode();

        var key = isLightMode ? "SECONDARY_COLOR_LIGHT" : "SECONDARY_COLOR_DARK";

        return (Color)application!.Resources[key];
    }

    public static Color GetLineColor(this Application application)
    {
        var isLightMode = application.IsLightMode();

        var key = isLightMode ? "LINES_COLOR_LIGHT" : "LINES_COLOR_DARK";

        return (Color)application!.Resources[key];
    }

    public static Color GetSkeletonViewColor(this Application application)
    {
        var isLightMode = application.IsLightMode();

        var key = isLightMode ? "SKELETON_LOADING_COLOR_LIGHT" : "SKELETON_LOADING_COLOR_DARK";

        return (Color)application!.Resources[key];
    }

    public static Color GetHighlightColor(this Application application)
    {
        var isLightMode = application.IsLightMode();

        var key = isLightMode ? "HIGHLIGHT_COLOR_LIGHT" : "HIGHLIGHT_COLOR_DARK";

        return (Color)application!.Resources[key];
    }

    public static Color GetDangerColor(this Application application)
    {
        var isLightMode = application.IsLightMode();

        var key = isLightMode ? "DANGER_ACTION_COLOR_LIGHT" : "DANGER_ACTION_COLOR_DARK";

        return (Color)application!.Resources[key];
    }
}
