using System.Collections.ObjectModel;
using System.ComponentModel;
using Tourist.ShipmentMobile.ViewModels;

namespace Tourist.ShipmentMobile;
public partial class CustomerPickerPage : ContentPage
{
    public CustomerPickerPage(CustomerPickerViewModel viewModel)
	{
		BindingContext = viewModel;

		InitializeComponent();
    }
}
