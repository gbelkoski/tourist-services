using System.Collections.ObjectModel;
using Tourist.ShipmentMobile.Infrastructure;
using Tourist.Domain;
using System.ComponentModel;
using System.Windows.Input;

namespace Tourist.ShipmentMobile.ViewModels;
public class CustomerPickerViewModel : BaseViewModel
{
	readonly ShipmentsDatabase _customerRepository;

    public CustomerPickerViewModel(ShipmentsDatabase customerRepository)
    {
		_customerRepository = customerRepository;
        CustomerSelectedCommand = new Command<Guid>(
        execute: (a) =>
        {
            Shell.Current.GoToAsync("//shipmentdetails");
        });
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

    public ICommand CustomerSelectedCommand { private set; get; }

    public async void LoadData()
    {
        var result = await _customerRepository.GetItemsAsync();

        Customers = new ObservableCollection<CustomerModel>();
        result.ForEach(r => Customers.Add(
            new CustomerModel()
            {
                Id = r.Id,
                Name = r.Name
            }));
    }

    public async Task SaveCustomer()
    {
        Customer newCustomer = new Domain.Customer()
        {
            Id = Guid.NewGuid(),
            Name = "Инекс Олгица",
            Address = "/"
        };
        await _customerRepository.SaveItemAsync(newCustomer);
    }
}

public class CustomerModel : BaseViewModel
{
    private Guid _id;
    public Guid Id
    {
        get { return _id; }
        set
        {
            _id = value;
            OnPropertyChanged("Id");
        }
    }

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