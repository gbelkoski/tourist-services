using System.Collections.ObjectModel;
using System.ComponentModel;
using Tourist.ShipmentMobile.ViewModels;

namespace Tourist.ShipmentMobile;
public partial class AdminMenuPage : ContentPage
{
    public AdminMenuPage()
	{
		InitializeComponent();
    }

    async void btnManageCustomers_Clicked(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync("//mainpage//adminmenu//managecustomers");
    }

    async void btnManageItems_Clicked(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync("//mainpage//adminmenu//manageitems");
    }
}
