namespace Tourist.EmployeeClient;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    async void btnNew_Clicked(System.Object sender, System.EventArgs e)
    {
        await Navigation.PushAsync(new CustomerPicker());
    }

    async void btnPending_Clicked(System.Object sender, System.EventArgs e)
    {
        await Navigation.PushAsync(new ShipmentsPending());
    }

    async void btnDelivered_Clicked(System.Object sender, System.EventArgs e)
    {
        await Navigation.PushAsync(new ShipmentsDelivered());
    }
}
