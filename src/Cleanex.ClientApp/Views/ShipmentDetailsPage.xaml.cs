using System.Collections.ObjectModel;
using Microsoft.Maui.Platform;
using Cleanex.ClientApp.Infrastructure;
using Cleanex.ClientApp.ViewModels;

namespace Cleanex.ClientApp;

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
		var printerService = DependencyService.Get<IPrintService>();
#if WINDOWS
		await Cleanex.ClientApp.Platforms.Windows.PrintService.Print(_viewModel.ShipmentItems.ToList());
#endif
		//printerService.Print(_viewModel.ShipmentItems.ToList());
	}
}
