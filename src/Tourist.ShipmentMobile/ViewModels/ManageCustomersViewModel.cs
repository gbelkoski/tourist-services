using System.Collections.ObjectModel;
using Tourist.ShipmentMobile.Infrastructure;
using Tourist.Domain;
using System.ComponentModel;
using System.Windows.Input;

namespace Tourist.ShipmentMobile.ViewModels;
public class ManageCustomersViewModel : BaseViewModel
{
	readonly ShipmentsDatabase _dataRepository;

    public ManageCustomersViewModel(ShipmentsDatabase dataRepository)
    {
        _dataRepository = dataRepository;
        CustomerSelectedCommand = new Command<CustomerModel>(
            execute: (a) =>
            {
                Shell.Current.GoToAsync($"//mainpage/adminmenu/managecustomers/addeditcustomer?customerId={a.Id}");
            });

        NewCustomerCommand = new Command(
            execute: () =>
            {
                Shell.Current.GoToAsync($"//mainpage/adminmenu/managecustomers/addeditcustomer?customerId=-1");
            });

        RefreshCustomersCommand = new Command(
            execute: () =>
            {
                LoadData();
            });

        Customers = new ObservableCollection<CustomerModel>();
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

    public ICommand NewCustomerCommand { private set; get; }

    public ICommand CustomerSelectedCommand { private set; get; }

    public ICommand RefreshCustomersCommand { private set; get; }

    public async void LoadData()
    {
        IsRefreshing = true;
        Customers.Clear();
        var result = await _dataRepository.GetCustomersAsync();
        result.ForEach(r => Customers.Add(
            new CustomerModel()
            {
                Id = r.Id,
                Name = r.Name
            }));

        IsRefreshing = false;
    }
}