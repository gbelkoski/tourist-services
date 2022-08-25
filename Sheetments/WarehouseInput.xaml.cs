using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sheetments
{
    /// <summary>
    /// Interaction logic for WarehouseInput.xaml
    /// </summary>
    public partial class WarehouseInput : UserControl
    {
        SheetmentContext _sheetmentContext;
        public WarehouseInput(SheetmentContext dbContext)
        {
            InitializeComponent();
            this.DataContext = this;
            _sheetmentContext = dbContext;
            var grouped = dbContext.ItemLedger.Where(b => !b.IsDeleted).Include(b => b.Customer).ToList()
                .GroupBy(a => new { a.Customer }).Select(b => new CustomerShipment()
                {
                    Customer = b.Key.Customer,
                    TotalWeigth = b.Sum(c => c.Weight),
                    Packages = new ObservableCollection<Package>(b.Select(c => new Package() { Barcode = c.Barcode, Weight = c.Weight }))
                }).ToList();

            LedgerByCustomer = new ObservableCollection<CustomerShipment>(grouped);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ObservableCollection<CustomerShipment> _ledgerByCustomer;
        public ObservableCollection<CustomerShipment> LedgerByCustomer
        {
            get { return _ledgerByCustomer; }
            set
            {
                _ledgerByCustomer = value;
                NotifyPropertyChanged();
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var barcode = (sender as TextBox).Text;
                if (BarcodeExists(barcode))
                {
                    MessageBox.Show("Баркодот веќе постои");
                    return;
                }
                if (!barcode.Any() || barcode.Split(' ').Length < 2)
                {
                    MessageBox.Show("Баркодот не е валиден");
                    return;
                }
                var customerName = BarcodeUtils.GetCustomer(barcode);
                var qty = BarcodeUtils.GetWeight(barcode);
                var existingCustomerGroup = LedgerByCustomer?.FirstOrDefault(c => c.Customer.Name == customerName);
                var customer = _sheetmentContext.Customers.FirstOrDefault(c => c.Name == customerName);

                if (customer == null)
                {
                    MessageBox.Show("Клиентот не постои");
                    return;
                }

                _sheetmentContext.ItemLedger.Add(new ItemLedger()
                {
                    Barcode = barcode,
                    CustomerId = customer.Id,
                    Weight = qty,
                    DateCreated = DateTime.Now
                });

                try
                {
                    var success = _sheetmentContext.SaveChanges();
                    if (success != 1)
                    {
                        MessageBox.Show("Неуспешен внес");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Неуспешен внес");
                    return;
                }

                if (existingCustomerGroup != null)
                {
                    existingCustomerGroup.Packages.Add(new Package { Barcode = barcode, Weight = qty });
                    existingCustomerGroup.TotalWeigth = existingCustomerGroup.TotalWeigth + qty;
                }
                else
                {
                    LedgerByCustomer.Add(new CustomerShipment() { Customer = new Customer { Name = customer.Name }, Packages = new ObservableCollection<Package>() { new Package { Barcode = barcode, Weight = qty } } });
                }

                txtBarcode.Focus();
            }
        }

        private bool BarcodeExists(string barcode)
        {
            return _sheetmentContext.ItemLedger.Any(i => i.Barcode == barcode);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Дали сте сигурни?", "", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                return;

            var dc = (sender as Button).DataContext as Package;
            if(dc == null || string.IsNullOrEmpty(dc.Barcode))
            {
                MessageBox.Show("Баркодот не може да се прочита");
                return;
            }
            if (BarcodeExists(dc.Barcode))
            {
                _sheetmentContext.ItemLedger.Remove(_sheetmentContext.ItemLedger.FirstOrDefault(i => i.Barcode == dc.Barcode));
                try
                {
                    var success = _sheetmentContext.SaveChanges();
                    if (success != 1)
                    {
                        MessageBox.Show("Неуспешно бришење");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Неуспешно бришење");
                    return;
                }

                var customer = BarcodeUtils.GetCustomer(dc.Barcode);
                var customerRecord = LedgerByCustomer.FirstOrDefault(l => l.Customer.Name == customer);
                var package = customerRecord.Packages.FirstOrDefault(c => c.Barcode == dc.Barcode);
                customerRecord.TotalWeigth = customerRecord.TotalWeigth - package.Weight;
                customerRecord.Packages.Remove(package);
            }
        }
    }
}
