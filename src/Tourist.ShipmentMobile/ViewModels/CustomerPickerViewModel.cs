using System.Collections.ObjectModel;
using Tourist.ShipmentMobile.Infrastructure;
using Tourist.Domain;
using System.ComponentModel;
using System.Windows.Input;

namespace Tourist.ShipmentMobile.ViewModels;
public class CustomerPickerViewModel : BaseViewModel
{
	readonly ShipmentsDatabase _dataRepository;

    public CustomerPickerViewModel(ShipmentsDatabase dataRepository)
    {
        _dataRepository = dataRepository;
        CustomerSelectedCommand = new Command<CustomerModel>(
            execute: (a) =>
            {
                //Shell.Current.GoToAsync("//shipmentdetails");
                Shell.Current.GoToAsync($"//mainpage/customerpicker/shipmentdetails?customerId={a.Id}&customerName={a.Name}");
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
        var result = await _dataRepository.GetCustomersAsync();

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
            Id = 200007,
            Name = "Инекс Олгица",
            Address = "/"
        };
        await _dataRepository.SaveCustomerAsync(newCustomer);
    }
}

public class CustomerModel : BaseViewModel
{
    private int _id;
    public int Id
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