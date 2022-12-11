using System.Collections.ObjectModel;
using System.ComponentModel;
using Tourist.ShipmentMobile.ViewModels;

namespace Tourist.ShipmentMobile;
public partial class PasswordPromptPage : ContentPage
{
    public PasswordPromptPage()
	{
		InitializeComponent();
    }

    async void btnNext_Clicked(System.Object sender, System.EventArgs e)
    {
        if(txtPass.Text == "CleanX0099")
        {
            await Shell.Current.GoToAsync("//mainpage//adminmenu");
        }
    }
}
