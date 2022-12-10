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

    ObservableCollection<ShipmentModel> _shipments;
    public ObservableCollection<ShipmentModel> Shipments
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

            Shipments = new ObservableCollection<ShipmentModel>();
            var filtered = result.GroupBy(r => new { r.ShipmentNo, r.CustomerId, r.DateShipped })
                  .Select(r =>
                    new ShipmentModel()
                    {
                        ShipmentNo = r.Key.ShipmentNo,
                        CustomerName = "Инекс олгица",// TO DO: Get from mapping table or join
                        DateShipped = r.Key.DateShipped.Value
                    }).ToList();

            filtered.ForEach(f => Shipments.Add(f));
        }
        catch(Exception ex)
        {

        }
    }
}

public class ShipmentModel
{
    public string ShipmentNo { get; set; }
    public DateTime DateShipped { get; set; }
    public string CustomerName { get; set; }
}