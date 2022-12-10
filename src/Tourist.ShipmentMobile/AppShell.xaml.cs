namespace Tourist.ShipmentMobile;

public partial class AppShell : Shell
{
	public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();

	public AppShell()
	{
        InitializeComponent();
        RegisterRoutes();
        BindingContext = this;
	}
	
    void RegisterRoutes()
    {
        Routes.Add("customerpicker", typeof(CustomerPickerPage));
        Routes.Add("shipmentdetails", typeof(ShipmentDetailsPage));
        Routes.Add("shipmentsdelivered", typeof(ShipmentsDeliveredPage));

        foreach (var item in Routes)
        {
            Routing.RegisterRoute(item.Key, item.Value);
        }
    }
}
