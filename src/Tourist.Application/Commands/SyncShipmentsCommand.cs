using System;
using Tourist.Domain;

namespace Tourist.Application.Commands;
public class SyncShipmentsCommand : ICommand
{
    public List<ShipmentLineItem> Shipments { get; set; }
}