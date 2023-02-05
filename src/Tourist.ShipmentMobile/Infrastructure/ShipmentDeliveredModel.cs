using System;
namespace Tourist.ShipmentMobile.Infrastructure;
public class ShipmentDeliveredModel
{
	public ShipmentDeliveredModel()
	{
	}

    public string ShipmentNo { get; set; }
    public DateTime DateShipped { get; set; }
    public string CustomerName { get; set; }
}
