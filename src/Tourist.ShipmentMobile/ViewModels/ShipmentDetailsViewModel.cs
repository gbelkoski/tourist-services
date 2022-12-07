﻿using System;
using System.Collections.ObjectModel;
using System.Text.Encodings.Web;
using System.Windows.Input;
using Tourist.ShipmentMobile.Infrastructure;

namespace Tourist.ShipmentMobile.ViewModels;
public class ShipmentDetailsViewModel : BaseViewModel, IQueryAttributable
{
    readonly ShipmentsDatabase _dataRepository;
    public ShipmentDetailsViewModel(ShipmentsDatabase dataRepository)
	{
        _dataRepository = dataRepository;
            BarcodeEnteredCommand = new Command(
            execute: async () =>
            {
                var itemId = GetItemId(Barcode);
                var weight = GetWeight(Barcode);
                var customerId = GetCustomerId(Barcode);
                if(customerId != SelectedCustomerId)
                {
                    // Invalid, show popup
                }

                await _dataRepository.SaveShipmentLineItemAsync(new Domain.ShipmentLineItem()
                {
                    Barcode = Barcode,
                    ShipmentNo = ShipmentNo,
                    CustomerId = customerId,
                    ItemId = itemId,
                    DateCreated = DateTime.Now,
                    Weight = weight
                });
                ShipmentItems.Add(new ShipmentItemModel()
                {
                    DateCreated = DateTime.Now,
                    ItemName = (await _dataRepository.GetItemAsync(itemId)).Name,
                    Weight = weight
                });
                Barcode = string.Empty;
            });
    }

    public ICommand BarcodeEnteredCommand { private set; get; }

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

    private string _barcode;
    public string Barcode
    {
        get { return _barcode; }
        set
        {
            _barcode = value;
            OnPropertyChanged("Barcode");
        }
    }

    public int SelectedCustomerId { get; set; }
    private string _selectedCustomerName;
    public string SelectedCustomerName
    {
        get { return _selectedCustomerName; }
        set
        {
            _selectedCustomerName = value;
            OnPropertyChanged("SelectedCustomerName");
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
        SelectedCustomerId = int.Parse(query["customerId"].ToString());
        SelectedCustomerName = query["customerName"].ToString();

        var activeShipmentItems = await _dataRepository.GetActiveShipmentAsync(SelectedCustomerId);
        ShipmentItems = new ObservableCollection<ShipmentItemModel>();
        if (activeShipmentItems.Any())
        {
            activeShipmentItems.ForEach(async s => ShipmentItems.Add(
                new ShipmentItemModel()
                {
                    DateCreated = s.DateCreated,
                    ItemName = (await _dataRepository.GetItemAsync(s.ItemId)).Name,
                    Weight = s.Weight
                }));
            ShipmentNo = activeShipmentItems.FirstOrDefault().ShipmentNo;
        }
        else
        {
            var last =  await _dataRepository.GetLatestShipedItemAsync(SelectedCustomerId);
            if(last == null)
            {
                ShipmentNo = "1";
            }
            else if(last.DateShipped.Value.Year == DateTime.Now.Year)
            {
                ShipmentNo = (int.Parse(last.ShipmentNo) + 1).ToString();
            }
            else if(last.DateShipped.Value.Year < DateTime.Now.Year)
            {
                ShipmentNo = "1";
            }
        }
    }

    private int GetCustomerId(string barcode)
    {
        string customerString = barcode.Substring(0, 6);
        return int.Parse(customerString);
    }

    private int GetItemId(string barcode)
    {
        string itemString = barcode.Substring(6, 2);
        return int.Parse(itemString);
    }

    private decimal GetWeight(string barcode)
    {
        var weightString = barcode.Substring(8, 4);
        decimal weigth = decimal.Parse(weightString.Substring(0, 2)) + decimal.Parse(weightString.Substring(2, 2)) / 100;
        return weigth;
    }
}

public class ShipmentItemModel
{
    public DateTime DateCreated { get; set; }
    public string ItemName { get; set; }
    public decimal Weight { get; set; }
}