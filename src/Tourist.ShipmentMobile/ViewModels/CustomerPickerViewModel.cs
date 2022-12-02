using System.Collections.ObjectModel;
using Tourist.ShipmentMobile.Infrastructure;
using Tourist.Domain;
using System.ComponentModel;

namespace Tourist.ShipmentMobile.ViewModels;
public class CustomerPickerViewModel : INotifyPropertyChanged
{
	readonly ShipmentsDatabase _customerRepository;
    public CustomerPickerViewModel(ShipmentsDatabase customerRepository)
    {
		_customerRepository = customerRepository;

		LoadData();
		//foreach (var item in result)
		//{
		//	Customers.Add(item);
		//}
		//customersList.ItemsSource = Customers;
    }

    private ObservableCollection<CustomerModel> _customers;

    public ObservableCollection<CustomerModel> Customers
    {
        get { return _customers; }
        set
        {
            _customers = value;
            OnPropertyChanged("Customers");
        }
    }

    public async void LoadData()
    {
        //Customer newCustomer = new Domain.Customer()
        //{
        //    Id = Guid.NewGuid(),
        //    Name = "Инекс Олгица",
        //    Address = "/"
        //};
        //await _customerRepository.SaveItemAsync(newCustomer);

        var result = await _customerRepository.GetItemsAsync();

        Customers = new ObservableCollection<CustomerModel>();
        result.ForEach(r => Customers.Add(new CustomerModel() { Name = r.Name }));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class CustomerModel : INotifyPropertyChanged
{
    private string _name;
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            OnPropertyChanged("Name");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        if (this.PropertyChanged != null)
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
}