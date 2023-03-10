using Plugin.Fingerprint.Abstractions;

namespace Tourist.ShipmentMobile;

public partial class MainPage : ContentPage
{
    readonly IFingerprint _fingerprint;

    public MainPage(IFingerprint fingerprint)
	{
        InitializeComponent();
        _fingerprint = fingerprint;
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
        var request = new AuthenticationRequestConfiguration("Validate that you have fingers", "Because without them you will not be able to access");
        var result = await _fingerprint.AuthenticateAsync(request);
        if (result.Authenticated)
        {
            await Shell.Current.GoToAsync("//mainpage//adminmenu");
        }
        else
        {
            await Shell.Current.GoToAsync("//mainpage//passwordprompt");
        }
    }
}
