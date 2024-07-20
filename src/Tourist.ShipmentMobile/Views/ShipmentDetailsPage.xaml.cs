using System.Collections.ObjectModel;
using Microsoft.Maui.Platform;
using Tourist.ShipmentMobile.Infrastructure;
using Tourist.ShipmentMobile.ViewModels;

namespace Tourist.ShipmentMobile;

public partial class ShipmentDetailsPage : ContentPage
{
    ShipmentDetailsViewModel _viewModel;
	readonly ShipmentsDatabase _dataRepository;
	public ShipmentDetailsPage(ShipmentDetailsViewModel viewModel, ShipmentsDatabase dataRepository)
	{
		_dataRepository = dataRepository;
		_viewModel = viewModel;
		BindingContext = viewModel;
        InitializeComponent();
        Loaded += ShipmentDetailsPage_Loaded;
    }

    private void ShipmentDetailsPage_Loaded(object sender, EventArgs e)
    {
        txtBarcode.Focus();
    }

	private async void Print_Clicked(object sender, EventArgs e)
	{
		await _dataRepository.MarkAsShippedAsync(_viewModel.SelectedCustomerId);
		Platforms.Droid.PrintService.Print(shipmentPrintView);
	}
}
