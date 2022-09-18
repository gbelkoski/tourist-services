namespace Tourist.Domain;
public class ShipmentLineItem
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateShipped { get; set; }// Will be used to determine status (pending or shipped)
    public string ShipmentNo { get; set;}
    public string Barcode { get; set; }
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    public int ItemId { get; set; }
    public virtual Item Item { get; set; }
    public decimal Weight { get; set; }
}
