using System;

namespace Sheetments
{
    public class ShippmentItem
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string ShipmentNo { get; set; }
        public string Barcode { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public decimal Weight { get; set; }
    }
}
