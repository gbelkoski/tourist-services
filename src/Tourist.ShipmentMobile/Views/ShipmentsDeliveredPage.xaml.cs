using Tourist.ShipmentMobile.ViewModels;

namespace Tourist.ShipmentMobile;

public partial class ShipmentsDeliveredPage : ContentPage
{
	public ShipmentsDeliveredPage(ShipmentsDeliveredViewModel viewModel)
	{
		this.BindingContext = viewModel;
		InitializeComponent();
	}
}
