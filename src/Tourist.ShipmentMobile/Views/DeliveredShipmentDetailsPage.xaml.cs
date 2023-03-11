using System.Collections.ObjectModel;
using Microsoft.Maui.Platform;
using Tourist.ShipmentMobile.ViewModels;

namespace Tourist.ShipmentMobile;

public partial class DeliveredShipmentDetailsPage : ContentPage
{
	public DeliveredShipmentDetailsPage(DeliveredShipmentDetailsViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
    }
}
