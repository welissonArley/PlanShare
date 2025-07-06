namespace PlanShare.App.Views.Components.Skeletons;

public partial class EntryAndLabelComponent : ContentView
{
	public static readonly BindableProperty TitleProperty = BindableProperty
		.Create(nameof(Title), typeof(string), typeof(EntryAndLabelComponent), string.Empty);

    public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }
	
    public EntryAndLabelComponent()
	{
		InitializeComponent();
	}
}