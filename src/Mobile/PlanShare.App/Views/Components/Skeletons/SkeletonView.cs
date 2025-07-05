using PlanShare.App.Extensions;

namespace PlanShare.App.Views.Components.Skeletons;
public class SkeletonView : BoxView
{
    private const uint ANIMATION_DURATION = 1500;
    private const uint ANIMATION_RATE = 20;

    public SkeletonView()
    {
        Color = Application.Current!.GetSkeletonViewColor();

        var smoothAnimation = new Animation();

        smoothAnimation.WithConcurrent(f => Opacity = f, start: 0.3, end: 1, Easing.SinIn);
        smoothAnimation.WithConcurrent(f => Opacity = f, start: 1, end: 0.3, Easing.SinInOut);

        this.Animate(name: "FadeInOut",
            animation: smoothAnimation,
            rate: ANIMATION_RATE,
            length: ANIMATION_DURATION,
            easing: Easing.SinIn,
            finished: null,
            repeat: () => true);
    }
}
