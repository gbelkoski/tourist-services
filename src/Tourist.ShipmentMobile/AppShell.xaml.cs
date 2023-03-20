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
        Routes.Add("deliveredshipments", typeof(DeliveredShipmentsPage));
        Routes.Add("deliveredshipmentdetails", typeof(DeliveredShipmentDetailsPage));

        // Admin section
        Routes.Add("passwordprompt", typeof(PasswordPromptPage));
        Routes.Add("adminmenu", typeof(AdminMenuPage));
        Routes.Add("managesettings", typeof(ManageSettingsPage));
        Routes.Add("managecustomers", typeof(ManageCustomersPage));
        Routes.Add("addeditcustomer", typeof(AddEditCustomerPage));
        Routes.Add("manageitems", typeof(ManageItemsPage));
        Routes.Add("addedititem", typeof(AddEditItemPage));

        foreach (var item in Routes)
        {
            Routing.RegisterRoute(item.Key, item.Value);
        }
    }
}
