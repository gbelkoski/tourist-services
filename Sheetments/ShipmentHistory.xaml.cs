using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Sheetments
{
    /// <summary>
    /// Interaction logic for ShipmentHistory.xaml
    /// </summary>
    public partial class ShipmentHistory : UserControl, INotifyPropertyChanged
    {
        public ShipmentHistory(SheetmentContext dbContext)
        {
            InitializeComponent();
            this.DataContext = this;
            var grouped = dbContext.ShippmentItem.Include(b => b.Customer).ToList().GroupBy(a => new { a.ShipmentNo, a.Customer }).Select(b => new Shippment()
            {
                ShippmentNo = b.Key.ShipmentNo,
                Customer = b.Key.Customer,
                DateCreated = b.FirstOrDefault().DateCreated,
                TotalWeight = b.Sum(c => c.Weight)
            }).ToList();
            Shippments = new ObservableCollection<Shippment>(grouped);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ObservableCollection<Shippment> _shippments;
        public ObservableCollection<Shippment> Shippments
        {
            get { return _shippments; }
            set
            {
                _shippments = value;
                NotifyPropertyChanged();
            }
        }
        public class Shippment
        {
            public DateTime DateCreated { get; set; }
            public string ShippmentNo { get; set; }
            public Customer Customer { get; set; }
            public decimal TotalWeight { get; set; }
        }
    }
}
