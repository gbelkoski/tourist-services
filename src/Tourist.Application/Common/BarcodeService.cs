namespace Tourist.Infrastructure
{
    public static class BarcodeService
    {
        public static string GetCustomer(string barcode)
        {
            throw new NotImplementedException();
        }

        public static string GetItemId(string barcode)
        {
            return barcode.Substring(5, 2);
        }

        public static decimal GetWeight(string barcode)
        {
            var weightString = barcode.Substring(7, 4);
            decimal weigth = decimal.Parse(weightString.Substring(0,2)) + decimal.Parse(weightString.Substring(2,2)) / 100;
            return weigth;
        }
    }
}
