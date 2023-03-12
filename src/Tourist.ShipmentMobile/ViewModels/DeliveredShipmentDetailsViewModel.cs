using System;
using System.Collections.ObjectModel;
using System.Text.Encodings.Web;
using System.Windows.Input;
using Tourist.ShipmentMobile.Infrastructure;
using Tourist.ShipmentMobile.Models;

namespace Tourist.ShipmentMobile.ViewModels;
public class DeliveredShipmentDetailsViewModel : BaseViewModel, IQueryAttributable
{
    readonly ShipmentsDatabase _dataRepository;
    public DeliveredShipmentDetailsViewModel(ShipmentsDatabase dataRepository)
	{
        _dataRepository = dataRepository;
    }


    public ICommand PrintShipmentCommand { private set; get; }

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

    private string _customerName;
    public string CustomerName
    {
        get { return _customerName; }
        set
        {
            _customerName = value;
            OnPropertyChanged("CustomerName");
        }
    }
    private string _shipmentNo;
    public string ShipmentNo
    {
        get { return _shipmentNo; }
        set
        {
            _shipmentNo = value;
            OnPropertyChanged("ShipmentNo");
        }
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var customerId = int.Parse(query["customerId"].ToString());
        var shipmentNo = query["shipmentNo"].ToString();
        var dateShipped = new DateTime(long.Parse(query["dateShipped"].ToString()));
        var shipmentItems = await _dataRepository.GetShipmentLineItemsAsync(shipmentNo, customerId, dateShipped);
        CustomerName = (await _dataRepository.GetCustomerAsync(customerId)).Name;
        ShipmentNo = shipmentNo;

        ShipmentItems = new ObservableCollection<ShipmentItemModel>();
        shipmentItems.ForEach(async s => ShipmentItems.Add(
            new ShipmentItemModel()
            {
                Id = s.Id,
                DateCreated = s.DateCreated,
                ItemName = (await _dataRepository.GetItemAsync(s.ItemId)).Name,
                Weight = s.Weight
            }));
    }
}