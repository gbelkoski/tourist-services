using Tourist.Domain;

namespace Tourist.Infrastructure;
public interface IShipmentRepository
{
    Task<List<ShipmentLineItem>> SelectActiveByCustomer(Guid customerId);
    Task<List<ShipmentLineItem>> SelectDelivered(string shipmentNo);
}