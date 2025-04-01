namespace PlanShare.App.Resources.Styles.Handlers;

public class CustomEntryHandler
{
    public static void Customize()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("PlanShareEntry", (handler, view) =>
        {

        });
    }
}
