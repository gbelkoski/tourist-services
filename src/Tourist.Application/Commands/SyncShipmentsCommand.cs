using System;
using Tourist.Domain;
using Tourist.Infrastructure;

namespace Tourist.Application.Commands;
public class SyncShipmentsCommand : ICommand
{
    public List<ShipmentLineItem> Shipments { get; set; }
}

public class SyncShipmentsCommandHandler : ICommandHandler<SyncShipmentsCommand>
{
    readonly IGenericRepository<ShipmentLineItem> _shipmentRepository;

    public SyncShipmentsCommandHandler(IGenericRepository<ShipmentLineItem> shipmentRepository)
    {
        _shipmentRepository = shipmentRepository;
    }

    public async Task HandleAsync(SyncShipmentsCommand command)
    {
        foreach (var item in command.Shipments)
        {
            var dbItem = await _shipmentRepository.SelectById(item.Id);
            if (dbItem == null)
            {
                await _shipmentRepository.Insert(new ShipmentLineItem()
                {
                    Id = item.Id,
                    CustomerId = item.CustomerId,
                    Barcode = item.Barcode,
                    ItemId = item.ItemId,
                    ShipmentNo = item.ShipmentNo,
                    Weight = item.Weight,
                    DateCreated = item.DateCreated,
                    DateShipped = item.DateShipped
                });
            }
            else
            {
                dbItem.CustomerId = item.CustomerId;
                dbItem.Barcode = item.Barcode;
                dbItem.ItemId = item.ItemId;
                dbItem.ShipmentNo = item.ShipmentNo;
                dbItem.Weight = item.Weight;
                dbItem.DateCreated = item.DateCreated;
                dbItem.DateShipped = item.DateShipped;
                await _shipmentRepository.Update(dbItem);
            }
        }
    }
}