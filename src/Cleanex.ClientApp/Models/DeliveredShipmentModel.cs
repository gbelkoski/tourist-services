namespace Cleanex.ClientApp.Models;
public class DeliveredShipmentModel
{
    public string ShipmentNo { get; set; }
    public DateTime DateShipped { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public bool IsDirty { get; set; }
}
