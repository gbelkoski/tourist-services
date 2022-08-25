using System.Collections.ObjectModel;

namespace Sheetments
{
    public class CustomerShipment : NotifyPropertyChangedBase
    {
        public CustomerShipment()
        {
            this.Packages = new ObservableCollection<Package>();
        }
        public string ShipmentNo { get; set; }
        public Customer Customer { get; set; }
        private decimal _totalWeigth { get; set; }
        public decimal TotalWeigth 
        { 
            get 
            {
                return _totalWeigth; 
            }
            set 
            { 
                _totalWeigth = value;
                NotifyPropertyChanged();
            } 
        }
        public ObservableCollection<Package> Packages { get; set; }
    }
}
