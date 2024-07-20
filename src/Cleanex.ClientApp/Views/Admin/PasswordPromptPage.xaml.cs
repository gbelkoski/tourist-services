using System.Collections.ObjectModel;
using System.ComponentModel;
using Cleanex.ClientApp.ViewModels;

namespace Cleanex.ClientApp;
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
