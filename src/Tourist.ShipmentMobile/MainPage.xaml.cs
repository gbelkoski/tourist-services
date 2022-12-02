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

    async void btnPending_Clicked(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync("//mainpage//shipmentspending");
    }

    async void btnDelivered_Clicked(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync("//mainpage//shipmentsdelivered");
    }
}
