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
            if(handler is not null
            && handler.PlatformView is not null
            && handler.PlatformView.TextCursorDrawable is not null
            && handler.PlatformView.Background is not null)
            {
                handler.PlatformView.TextCursorDrawable.SetTint(cursorColor.ToPlatform());
                handler.PlatformView.Background.SetTint(lineColor.ToPlatform());
            }
#endif
        });
    }
}
