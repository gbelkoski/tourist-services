
using System;
using System.Collections.ObjectModel;

namespace Tourist.ShipmentMobile.ViewModels;
public class ShipmentsPendingViewModel : BaseViewModel
{
    public ShipmentsPendingViewModel()
    {
    }

    private ObservableCollection<ShipmentModel> _shipments;
    public ObservableCollection<ShipmentModel> Shipments
    {
        get { return _shipments; }
        set
        {
            _shipments = value;
            OnPropertyChanged("Shipments");
        }
    }
}

public class ShipmentModel : BaseViewModel
{
    public string CustomerName { get; set; }
}