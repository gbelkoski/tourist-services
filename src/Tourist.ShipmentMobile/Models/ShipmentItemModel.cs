namespace Tourist.ShipmentMobile.Models;
public class ShipmentItemModel
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public string ItemName { get; set; }
    public decimal Weight { get; set; }
}