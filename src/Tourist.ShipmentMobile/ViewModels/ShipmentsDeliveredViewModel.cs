using System;
using System.Collections.ObjectModel;
using Tourist.ShipmentMobile.Infrastructure;

namespace Tourist.ShipmentMobile.ViewModels;
public class ShipmentsDeliveredViewModel : BaseViewModel
{
    readonly ShipmentsDatabase _dataRepository;
    public ShipmentsDeliveredViewModel(ShipmentsDatabase dataRepository)
    {
        _dataRepository = dataRepository;

        LoadData();
    }

    ObservableCollection<ShipmentDeliveredModel> _shipments;
    public ObservableCollection<ShipmentDeliveredModel> Shipments
    {
        get { return _shipments; }
        set
        {
            _shipments = value;
            OnPropertyChanged("Shipments");
        }
    }

    public async void LoadData()
    {
        try
        {
            var result = await _dataRepository.GetDeliveredShipmentsAsync();

            Shipments = new ObservableCollection<ShipmentDeliveredModel>();
            var filtered = result.GroupBy(r => new { r.ShipmentNo, r.CustomerName, r.DateShipped })
                  .Select(r =>
                    new ShipmentDeliveredModel()
                    {
                        ShipmentNo = r.Key.ShipmentNo,
                        CustomerName = r.Key.CustomerName,
                        DateShipped = r.Key.DateShipped
                    }).ToList();

            filtered.ForEach(f => Shipments.Add(f));
        }
        catch(Exception ex)
        {

        }
    }
}