using Microsoft.Maui.Platform;
using PlanShare.App.Extensions;

namespace PlanShare.App.Resources.Styles.Handlers;

public class CustomEntryHandler
{
    public static void Customize()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("PlanShareEntry", (handler, entry) =>
        {
            var cursorColor = Application.Current!.GetPrimaryColor();
            var lineColor = Application.Current!.GetLineColor();

#if ANDROID
            handler?.PlatformView?.TextCursorDrawable?.SetTint(cursorColor.ToPlatform());
            handler?.PlatformView?.Background?.SetTint(lineColor.ToPlatform());
#elif IOS || MACCATALYST
            handler.PlatformView.Layer.BorderColor = lineColor.ToCGColor();
            handler.PlatformView.Layer.BorderWidth = 0.7f;
            handler.PlatformView.Layer.CornerRadius = 5;
            handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
            handler.PlatformView.TintColor = cursorColor.ToPlatform();
#endif
        });
    }
}
