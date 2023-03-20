using Tourist.ShipmentMobile.ViewModels;

namespace Tourist.ShipmentMobile;
public partial class ManageSettingsPage : ContentPage
{
	public ManageSettingsPage(ManageSettingsViewModel viewModel)
	{
		this.BindingContext = viewModel;
		InitializeComponent();
	}
}
