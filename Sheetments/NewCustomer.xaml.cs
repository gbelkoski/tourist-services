using System;
using System.Collections.Generic;
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
    /// Interaction logic for NewCustomer.xaml
    /// </summary>
    public partial class NewCustomer : UserControl
    {
        private readonly SheetmentContext dbContext;
        public NewCustomer(SheetmentContext dbContext)
        {
            InitializeComponent();
            this.DataContext = this;
            this.dbContext = dbContext;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Customer newCustomer = new Customer()
            {
                Name = txtName.Text
            };
            this.dbContext.Add(newCustomer);
            dbContext.SaveChanges();
        }
    }
}
