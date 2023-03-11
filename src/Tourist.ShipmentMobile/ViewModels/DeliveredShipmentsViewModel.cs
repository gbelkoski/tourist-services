using System;
using System.Collections.ObjectModel;
using Tourist.ShipmentMobile.Infrastructure;
using Tourist.ShipmentMobile.Models;

namespace Tourist.ShipmentMobile.ViewModels;
public class DeliveredShipmentsViewModel : BaseViewModel
{
    readonly ShipmentsDatabase _dataRepository;
    public DeliveredShipmentsViewModel(ShipmentsDatabase dataRepository)
    {
        _dataRepository = dataRepository;

        LoadData();
    }

    ObservableCollection<DeliveredShipmentModel> _shipments;
    public ObservableCollection<DeliveredShipmentModel> Shipments
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

            Shipments = new ObservableCollection<DeliveredShipmentModel>();
            var filtered = result.GroupBy(r => new { r.ShipmentNo, r.CustomerName, r.CustomerId, r.DateShipped })
                  .Select(r =>
                    new DeliveredShipmentModel()
                    {
                        ShipmentNo = r.Key.ShipmentNo,
                        CustomerId = r.Key.CustomerId,
                        CustomerName = r.Key.CustomerName,
                        DateShipped = r.Key.DateShipped
                    }).ToList();

            filtered.ForEach(f => Shipments.Add(f));
        }
        catch { }
    }
}