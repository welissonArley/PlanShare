namespace PlanShare.App.Views.Components.Inputs;

public partial class EntryAndLabelPasswordComponent : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty
        .Create(nameof(Title), typeof(string), typeof(EntryAndLabelPasswordComponent), string.Empty);

    public static readonly BindableProperty TextValueProperty = BindableProperty
        .Create(nameof(TextValue), typeof(string), typeof(EntryAndLabelPasswordComponent), string.Empty, BindingMode.TwoWay);

    public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }
    public string TextValue { get => (string)GetValue(TextValueProperty); set => SetValue(TextValueProperty, value); }

    public EntryAndLabelPasswordComponent()
	{
		InitializeComponent();
	}

    private void ShowHidePassword(object sender, TappedEventArgs e)
    {
        if (EntryPassword.IsPassword)
        {
            EntryPassword.IsPassword = false;
            ImageEye.Source = "icon_eye.png";
        }
        else
        {
            EntryPassword.IsPassword = true;
            ImageEye.Source = "icon_eye_hidden.png";
        }
    }
}