using Cleanex.ClientApp.ViewModels;

namespace Cleanex.ClientApp;

public partial class AddShipmentLineItemPage : ContentPage
{
	public AddShipmentLineItemPage(AddShipmentLineItemViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
	}
}