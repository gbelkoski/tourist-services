using SQLite;

namespace Tourist.Domain;
public class ShipmentLineItem
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateShipped { get; set; }// Will be used to determine status (pending or shipped)
    public string ShipmentNo { get; set;}
    public string Barcode { get; set; }
    [Indexed]
    public int CustomerId { get; set; }
    public int ItemId { get; set; }
    public decimal Weight { get; set; }
}
