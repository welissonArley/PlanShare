namespace PlanShare.App.Extensions;
public static class DeviceDisplayExtensions
{
    private const double PERCENTAGE_WIDTH_OF_POPUP_ON_SCREEN = 0.8;

    public static double GetWidthForPopup(this IDeviceDisplay deviceDisplay)
    {
        var screenWidth = deviceDisplay.MainDisplayInfo.Width / deviceDisplay.MainDisplayInfo.Density;

        return screenWidth * PERCENTAGE_WIDTH_OF_POPUP_ON_SCREEN;
    }
}
