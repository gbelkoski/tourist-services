using System.Collections.ObjectModel;
using Tourist.ShipmentMobile;
using Tourist.ShipmentMobile.ViewModels;

namespace Tourist.ShipmentMobile;

public partial class ShipmentsPendingPage : ContentPage
{
	public ShipmentsPendingPage(ShipmentsPendingViewModel viewModel)
	{
        this.BindingContext = viewModel;
		InitializeComponent();
    }
}
