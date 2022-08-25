using System.Windows;

namespace Sheetments
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SheetmentContext dbContext;
        public MainWindow()
        {
            InitializeComponent();

        }

        private void btnWarehouseIn_Click(object sender, RoutedEventArgs e)
        {
            dbContext = new SheetmentContext();
            content.Content = new WarehouseInput(dbContext);
        }

        private void btnWarehouseOut_Click(object sender, RoutedEventArgs e)
        {
            content.Content = new Shippment();
        }

        private void btnShipmentHistory_Click(object sender, RoutedEventArgs e)
        {
            dbContext = new SheetmentContext();
            content.Content = new ShipmentHistory(dbContext);
        }

        private void btnNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            dbContext = new SheetmentContext();
            Window newCustomerW = new Window() { Height=200, Width=300 };
            newCustomerW.Content = new NewCustomer(dbContext);
            newCustomerW.Show();
        }
    }
}
