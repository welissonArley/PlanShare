using CommunityToolkit.Maui.Views;
using PlanShare.App.Extensions;
using PlanShare.App.ViewModels.Popups.Connection;

namespace PlanShare.App.Views.Popups.Connection;

public partial class OptionsForConnectionByCodePopup : Popup
{
	public OptionsForConnectionByCodePopup(OptionsForConnectionByCodeViewModel viewModel, IDeviceDisplay deviceDisplay)
	{
		InitializeComponent();

		BindingContext = viewModel;

		WidthRequest = deviceDisplay.GetWidthForPopup();
    }
}