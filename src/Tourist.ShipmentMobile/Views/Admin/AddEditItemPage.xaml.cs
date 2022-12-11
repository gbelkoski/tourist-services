using System.Collections.ObjectModel;
using System.ComponentModel;
using Tourist.ShipmentMobile.ViewModels;

namespace Tourist.ShipmentMobile;
public partial class AddEditItemPage : ContentPage
{
    public AddEditItemPage(AddEditItemViewModel viewModel)
	{
        BindingContext = viewModel;
		InitializeComponent();
    }
}
