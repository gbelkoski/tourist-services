using Tourist.ShipmentMobile.ViewModels;

namespace Tourist.ShipmentMobile;

public partial class AddShipmentLineItemPage : ContentPage
{
	public AddShipmentLineItemPage(AddShipmentLineItemViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
	}
}