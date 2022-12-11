using System.Collections.ObjectModel;
using System.ComponentModel;
using Tourist.ShipmentMobile.ViewModels;

namespace Tourist.ShipmentMobile;
public partial class AddEditCustomerPage : ContentPage
{
    public AddEditCustomerPage(AddEditCustomerViewModel viewModel)
	{
        BindingContext = viewModel;
		InitializeComponent();
    }
}
