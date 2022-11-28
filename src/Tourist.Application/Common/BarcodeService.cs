namespace Tourist.Infrastructure
{
    public static class BarcodeService
    {
        public static string GetCustomer(string barcode)
        {
            var strarr = barcode.Split(' ');
            return strarr[0];
        }
        public static decimal GetWeight(string barcode)
        {
            var strarr = barcode.Split(' ');
            return decimal.Parse(strarr[1]);
        }
    }
}
