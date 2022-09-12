namespace Tourist.Domain;
public class ShippmentLineItem
{
    private Customer customer;
    private Item item;
    private string barcode;
    private string shipmentNo;

    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateShipped { get; set; }// Will be used to determine status (pending or shipped)
    public string ShipmentNo { get => shipmentNo; set => shipmentNo = value; }
    public string Barcode { get => barcode; set => barcode = value; }
    public int CustomerId { get; set; }
    public virtual Customer Customer { get => customer; set => customer = value; }
    public int ItemId { get; set; }
    public virtual Item Item { get => item; set => item = value; }
    public decimal Weight { get; set; }
}
