using System.Collections.ObjectModel;
using Microsoft.Maui.Platform;
using Cleanex.ClientApp.ViewModels;

namespace Cleanex.ClientApp;

public partial class DeliveredShipmentDetailsPage : ContentPage
{
	public DeliveredShipmentDetailsPage(DeliveredShipmentDetailsViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
    }
}
