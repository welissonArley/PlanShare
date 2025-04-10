namespace PlanShare.App.Views.Components.Inputs;

public partial class EntryAndLabelComponent : ContentView
{
	public static readonly BindableProperty TitleProperty = BindableProperty
		.Create(nameof(Title), typeof(string), typeof(EntryAndLabelComponent), string.Empty);

    public static readonly BindableProperty PlaceholderProperty = BindableProperty
        .Create(nameof(Placeholder), typeof(string), typeof(EntryAndLabelComponent), string.Empty);

    public static readonly BindableProperty KeyboardProperty = BindableProperty
        .Create(nameof(Keyboard), typeof(Keyboard), typeof(EntryAndLabelComponent), Keyboard.Default);

    public static readonly BindableProperty TextValueProperty = BindableProperty
        .Create(nameof(TextValue), typeof(string), typeof(EntryAndLabelComponent), string.Empty, BindingMode.TwoWay);

    public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }
	public string Placeholder { get => (string)GetValue(PlaceholderProperty); set => SetValue(PlaceholderProperty, value); }
    public Keyboard Keyboard { get => (Keyboard)GetValue(KeyboardProperty); set => SetValue(KeyboardProperty, value); }
    public string TextValue { get => (string)GetValue(TextValueProperty); set => SetValue(TextValueProperty, value); }

    public EntryAndLabelComponent()
	{
		InitializeComponent();
	}
}