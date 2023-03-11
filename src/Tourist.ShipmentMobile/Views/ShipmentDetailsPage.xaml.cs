using System.Collections.ObjectModel;
using Microsoft.Maui.Platform;
using Tourist.ShipmentMobile.ViewModels;

namespace Tourist.ShipmentMobile;

public partial class ShipmentDetailsPage : ContentPage
{
	public ShipmentDetailsPage(ShipmentDetailsViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
        Loaded += ShipmentDetailsPage_Loaded;
    }

    private void ShipmentDetailsPage_Loaded(object sender, EventArgs e)
    {
        txtBarcode.Focus();
    }
}
