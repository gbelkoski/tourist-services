using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Tourist.EmployeeClient;

public partial class CustomerPicker : ContentPage, INotifyPropertyChanged
{
	public CustomerPicker()
	{
		InitializeComponent();

        this.BindingContext = this;

		Customers.Add(new Customer { Name = "cimer", Address = "kej Marshal tito" });
		Customers.Add(new Customer { Name = "chuki", Address = "/" });
		customersList.ItemsSource = Customers;
    }

    ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
    public ObservableCollection<Customer> Customers { get { return customers; } }

    void customersList_ItemTapped(System.Object sender, Microsoft.Maui.Controls.ItemTappedEventArgs e)
    {
		Navigation.PushAsync(new ShipmentDetails(new Guid(), string.Empty));
    }
}


public class Customer
{
	public string Name { get; set; }
	public string Address { get; set; }
}