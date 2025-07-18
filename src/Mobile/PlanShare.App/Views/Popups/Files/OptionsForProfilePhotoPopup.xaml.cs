using CommunityToolkit.Maui.Views;
using PlanShare.App.ViewModels.Popups.Files;

namespace PlanShare.App.Views.Popups.Files;

public partial class OptionsForProfilePhotoPopup : Popup
{
	public OptionsForProfilePhotoPopup(OptionsForProfilePhotoViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;

		var scheenWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;

		WidthRequest = scheenWidth * 0.8;
    }
}