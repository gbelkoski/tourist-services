using System;
using System.Collections.ObjectModel;

namespace Tourist.ShipmentMobile.ViewModels;
public class ShipmentDetailsViewModel : BaseViewModel
{
	public ShipmentDetailsViewModel()
	{
	}

    ObservableCollection<ShipmentItemModel> _shipmentItems;
    public ObservableCollection<ShipmentItemModel> ShipmentItems
    {
        get { return _shipmentItems; }
        set
        {
            _shipmentItems = value;
            OnPropertyChanged("ShipmentItems");
        }
    }

    public async void LoadData()
    {

    }
}

public class ShipmentItemModel
{
    public string Barcode { get; set; }
    public double Weight { get; set; }
    public int Type { get; set; }
    public string TypeString { get; set; }
}