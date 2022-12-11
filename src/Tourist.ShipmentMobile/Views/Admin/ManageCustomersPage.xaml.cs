using System.Collections.ObjectModel;
using System.ComponentModel;
using Tourist.ShipmentMobile.ViewModels;

namespace Tourist.ShipmentMobile;
public partial class ManageCustomersPage : ContentPage
{
    public ManageCustomersPage(ManageCustomersViewModel viewModel)
	{
        BindingContext = viewModel;
		InitializeComponent();
    }
}
