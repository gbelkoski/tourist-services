using System.Collections.ObjectModel;
using Tourist.ShipmentMobile.ViewModels;

namespace Tourist.ShipmentMobile;

public partial class ShipmentDetailsPage : ContentPage
{
	public ShipmentDetailsPage(ShipmentDetailsViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
    }
}
