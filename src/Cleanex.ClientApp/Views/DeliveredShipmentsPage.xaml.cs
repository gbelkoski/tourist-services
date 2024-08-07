﻿using Cleanex.ClientApp.ViewModels;
using Cleanex.ClientApp.Models;

namespace Cleanex.ClientApp;
public partial class DeliveredShipmentsPage : ContentPage
{
	public DeliveredShipmentsPage(DeliveredShipmentsViewModel viewModel)
	{
		this.BindingContext = viewModel;
		InitializeComponent();
	}

    void ListView_ItemTapped(System.Object sender, Microsoft.Maui.Controls.ItemTappedEventArgs e)
    {
        DeliveredShipmentModel item = e.Item as DeliveredShipmentModel;
        if(item != null)
        {
            Shell.Current.GoToAsync($"//mainpage/deliveredshipments/deliveredshipmentdetails?shipmentNo={item.ShipmentNo}&customerId={item.CustomerId}&dateShipped={item.DateShipped.Ticks}");
        }
    }
}
