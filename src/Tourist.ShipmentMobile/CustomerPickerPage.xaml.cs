using System.Collections.ObjectModel;
using System.ComponentModel;
using Tourist.ShipmentMobile.ViewModels;

namespace Tourist.ShipmentMobile;
public partial class CustomerPickerPage : ContentPage, INotifyPropertyChanged
{
    public CustomerPickerPage(CustomerPickerViewModel viewModel)
	{
		BindingContext = viewModel;

		InitializeComponent();
    }

    void customersList_ItemTapped(System.Object sender, Microsoft.Maui.Controls.ItemTappedEventArgs e)
    {
		//Navigation.PushAsync(new ShipmentDetails(new Guid(), string.Empty));
    }
}


