using System.Collections.ObjectModel;
using Tourist.ShipmentMobile;
using Tourist.ShipmentMobile.ViewModels;

namespace Tourist.ShipmentMobile;

public partial class ShipmentsPending : ContentPage
{
	public ShipmentsPending(ShipmentsPendingViewModel viewModel)
	{
        this.BindingContext = viewModel;
		InitializeComponent();
    }
}

public class Shipment
{
    public string CustomerName { get; set; }
}
