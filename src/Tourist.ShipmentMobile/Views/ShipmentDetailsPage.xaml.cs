using System.Collections.ObjectModel;
using Tourist.ShipmentMobile.ViewModels;

namespace Tourist.ShipmentMobile;

public partial class ShipmentDetailsPage : ContentPage
{
	public ShipmentDetailsPage(ShipmentDetailsViewModel viewModel)
	{
        BindingContext = viewModel;
        // TO DO: If customer id is filled, find pending shipment if any, if not open new and generate new number
        // If shipmentNo is not null open the shipment and set mode readonly
        InitializeComponent();
    }

    void Barcode_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {

    }
}
