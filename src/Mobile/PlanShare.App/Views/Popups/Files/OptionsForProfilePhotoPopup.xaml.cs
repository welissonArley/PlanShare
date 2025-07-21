using CommunityToolkit.Maui.Views;
using PlanShare.App.Extensions;
using PlanShare.App.ViewModels.Popups.Files;

namespace PlanShare.App.Views.Popups.Files;

public partial class OptionsForProfilePhotoPopup : Popup
{
	public OptionsForProfilePhotoPopup(OptionsForProfilePhotoViewModel viewModel, IDeviceDisplay deviceDisplay)
	{
		InitializeComponent();

		BindingContext = viewModel;

		WidthRequest = deviceDisplay.GetWidthForPopup();
    }
}