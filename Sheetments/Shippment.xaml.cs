using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sheetments
{
    /// <summary>
    /// Interaction logic for Shippment.xaml
    /// </summary>
    public partial class Shippment : UserControl, INotifyPropertyChanged
    {
        SheetmentContext _sheetmentContext;
        public Shippment(SheetmentContext dbContext)
        {
            InitializeComponent();
            this.DataContext = this;
            _sheetmentContext = dbContext;
            CustomerShipment root = new CustomerShipment() { Customer = new Customer { Name = "Клиент 1" } };
            Shipments = new ObservableCollection<CustomerShipment>() { root };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ObservableCollection<CustomerShipment> _mainItems;
        public ObservableCollection<CustomerShipment> Shipments
        {
            get { return _mainItems; }
            set
            {
                _mainItems = value;
                NotifyPropertyChanged();
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                var barcode = (sender as TextBox).Text;
                var customerName = GetCustomer(barcode);
                var qty = GetQty(barcode);
                var existingShipment = Shipments.FirstOrDefault(c => c.Customer.Name == customerName);
                var customer = _sheetmentContext.Customers.FirstOrDefault(c => c.Name == customerName);

                if (existingShipment != null)
                {
                    existingShipment.Packages.Add(new Package { Weight = qty });
                }
                else
                {
                    Shipments.Add(new CustomerShipment() { 
                        ShipmentNo = GetShipmentNumber(customer.Id), 
                        Customer =new Customer { 
                            Name = customer.Name 
                        }, 
                        Packages = new ObservableCollection<Package>() { 
                            new Package { Barcode = barcode, Weight = qty 
                            } 
                        } 
                    });
                }
            }
        }

        private string GetShipmentNumber(int customerId)
        {
            var shipmentNos = _sheetmentContext.ShippmentItem
                .Where(s => s.DateCreated.Year == DateTime.Now.Year 
                && s.CustomerId == customerId).GroupBy(s => s.ShipmentNo)
                .Count();

            return shipmentNos.ToString() + "/" + DateTime.Now.Year.ToString();
        }

        private string GetCustomer(string barcode)
        {
            var strarr = barcode.Split(' ');
            return strarr[0];
        }
        private decimal GetQty(string barcode)
        {
            var strarr = barcode.Split(' ');
            return decimal.Parse(strarr[1]);
        }
    }
}
