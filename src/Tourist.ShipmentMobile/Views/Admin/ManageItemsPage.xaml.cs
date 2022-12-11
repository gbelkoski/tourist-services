using System.Collections.ObjectModel;
using System.ComponentModel;
using Tourist.ShipmentMobile.ViewModels;

namespace Tourist.ShipmentMobile;
public partial class ManageItemsPage : ContentPage
{
    public ManageItemsPage(ManageItemsViewModel viewModel)
	{
        BindingContext = viewModel;
		InitializeComponent();
    }

}
