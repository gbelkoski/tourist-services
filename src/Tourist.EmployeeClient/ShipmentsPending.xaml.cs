using System.Collections.ObjectModel;
using Tourist.EmployeeClient;

namespace Tourist.EmployeeClient;

public partial class ShipmentsPending : ContentPage
{
	public ShipmentsPending()
	{
		InitializeComponent();
        ShipmentItems.Add(new Shipment { CustomerName = "Хотел..." });
        ShipmentItems.Add(new Shipment { CustomerName = "Апартмани..." });

        shipmentList.ItemsSource = ShipmentItems;
    }
    ObservableCollection<Shipment> shipmentItems = new ObservableCollection<Shipment>();
    public ObservableCollection<Shipment> ShipmentItems { get { return shipmentItems; } }

    void shipmentList_ItemTapped(System.Object sender, Microsoft.Maui.Controls.ItemTappedEventArgs e)
    {
    }
}

public class Shipment
{
    public string CustomerName { get; set; }
}
