using System.Collections.ObjectModel;

namespace Tourist.EmployeeClient;

public partial class ShipmentDetails : ContentPage
{
	public ShipmentDetails(Guid customerId, string shipmentNo)
	{
        // TO DO: If customer id is filled, find pending shipment if any, if not open new and generate new number
        // If shipmentNo is not null open the shipment and set mode readonly
		InitializeComponent();
        this.BindingContext = this;

        ShipmentItems.Add(new ShipmentItem { TypeString = "Чаршави", Weight = 2.50 });
        ShipmentItems.Add(new ShipmentItem { TypeString = "Пешкири", Weight = 3.2 });
        lineItemsList.ItemsSource = ShipmentItems;
    }

    ObservableCollection<ShipmentItem> shipmentItems = new ObservableCollection<ShipmentItem>();
    public ObservableCollection<ShipmentItem> ShipmentItems { get { return shipmentItems; } }

    void Barcode_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {

    }
}


public class ShipmentItem
{
    public string Barcode { get; set; }
    public double Weight { get; set; }
    public int Type { get; set; }
    public string TypeString { get; set; }
}