using PlanShare.App.Extensions;

namespace PlanShare.App.Views.Components.Skeletons;
public class SkeletonView : BoxView
{
    public SkeletonView()
    {
        Color = Application.Current!.GetSkeletonViewColor();


    }
}
