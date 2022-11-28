using Tourist.Domain;

namespace Tourist.Infrastructure;
public interface IShipmentRepository
{
    Task<ShipmentLineItem> Insert(ShipmentLineItem model);
    Task<List<ShipmentLineItem>> SelectActiveByCustomer(Guid customerId);
    Task<List<ShipmentLineItem>> SelectDelivered(string shipmentNo);
}