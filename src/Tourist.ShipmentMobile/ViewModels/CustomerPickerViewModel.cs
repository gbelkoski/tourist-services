using System.Collections.ObjectModel;
using Tourist.ShipmentMobile.Infrastructure;
using Tourist.Domain;
using System.ComponentModel;

namespace Tourist.ShipmentMobile.ViewModels;
public class CustomerPickerViewModel : BaseViewModel
{
	readonly ShipmentsDatabase _customerRepository;
    public CustomerPickerViewModel(ShipmentsDatabase customerRepository)
    {
		_customerRepository = customerRepository;

		LoadData();
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
        var result = await _customerRepository.GetItemsAsync();

        Customers = new ObservableCollection<CustomerModel>();
        result.ForEach(r => Customers.Add(new CustomerModel() { Name = r.Name }));
    }

    public async Task SaveCustomer()
    {
        //Customer newCustomer = new Domain.Customer()
        //{
        //    Id = Guid.NewGuid(),
        //    Name = "Инекс Олгица",
        //    Address = "/"
        //};
        //await _customerRepository.SaveItemAsync(newCustomer);
    }
}

public class CustomerModel : BaseViewModel
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
}