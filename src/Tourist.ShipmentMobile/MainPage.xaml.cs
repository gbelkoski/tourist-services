namespace Tourist.ShipmentMobile;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    async void btnNewShipment_Clicked(System.Object sender, System.EventArgs e)
    {
		await Shell.Current.GoToAsync("//mainpage//customerpicker");
    }

    async void btnDelivered_Clicked(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync("//mainpage//shipmentsdelivered");
    }

    async void btnAdmin_Clicked(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync("//mainpage//passwordprompt");
    }
}
