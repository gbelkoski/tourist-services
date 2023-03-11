using System;
using System.Windows.Input;
using Tourist.Domain;
using Tourist.ShipmentMobile.Infrastructure;

namespace Tourist.ShipmentMobile.ViewModels;
public class AddEditCustomerViewModel : BaseViewModel, IQueryAttributable
{
    readonly ShipmentsDatabase _dataRepository;
    public AddEditCustomerViewModel(ShipmentsDatabase dataRepository)
    {
        _dataRepository = dataRepository;
        SaveCustomerCommand = new Command(
            execute: async () =>
            {
                if(IsNew)
                {
                    Customer customer = new Customer()
                    {
                        Id = int.Parse(Id),
                        Name = Name,
                        Address = Address
                    };
                    await dataRepository.SaveCustomerAsync(customer);
                    IsNew = false;
                }
                else
                {
                    var customer = await dataRepository.GetCustomerAsync(customerId);
                    customer.Name = Name;
                    customer.Address = Address;
                    try
                    {
                        await dataRepository.UpdateCustomerAsync(customer);
                    }
                    catch { }
                }

                await Shell.Current.GoToAsync("//mainpage//adminmenu//managecustomers");
            });
    }

    private int customerId;

    public ICommand SaveCustomerCommand { private set; get; }

    private string _id;
    public string Id
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

    private string _address;
    public string Address
    {
        get { return _address; }
        set
        {
            _address = value;
            OnPropertyChanged("Address");
        }
    }

    private bool _isNew;
    public bool IsNew
    {
        get { return _isNew; }
        set
        {
            _isNew = value;
            OnPropertyChanged("IsNew");
        }
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        customerId = int.Parse(query["customerId"].ToString());

        if(customerId == -1)
        {
            IsNew = true;
        }
        else
        {
            var customer = await _dataRepository.GetCustomerAsync(customerId);
            Id = customer.Id.ToString();
            Name = customer.Name;
            Address = customer.Address;
        }
    }
}
