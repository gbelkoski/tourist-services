using System;
using System.Collections.ObjectModel;
using Cleanex.ClientApp.Infrastructure;
using Cleanex.ClientApp.Models;
using System.Windows.Input;

namespace Cleanex.ClientApp.ViewModels;
public class DeliveredShipmentsViewModel : BaseViewModel
{
    readonly ShipmentsDatabase _dataRepository;
    public DeliveredShipmentsViewModel(ShipmentsDatabase dataRepository)
    {
        _dataRepository = dataRepository;

        RefreshShipmentsCommand = new Command(
            execute: () =>
            {
                LoadData();
            });

        Shipments = new ObservableCollection<DeliveredShipmentModel>();
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

    public ICommand RefreshShipmentsCommand { private set; get; }

    public async void LoadData()
    {
        try
        {
            IsRefreshing = true;
            var result = await _dataRepository.GetDeliveredShipmentsAsync();

            var filtered = result.GroupBy(r => new { r.ShipmentNo, r.CustomerName, r.CustomerId, r.DateShipped, r.IsDirty })
                  .Select(r =>
                    new DeliveredShipmentModel()
                    {
                        ShipmentNo = r.Key.ShipmentNo,
                        CustomerId = r.Key.CustomerId,
                        CustomerName = r.Key.CustomerName,
                        DateShipped = r.Key.DateShipped,
                        IsDirty = r.Key.IsDirty
                    }).ToList();
            Shipments.Clear();
            filtered.ForEach(f => Shipments.Add(f));
            IsRefreshing = false;
        }
        catch { }
    }
}