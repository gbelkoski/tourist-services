using System;
using System.Windows.Input;
using Tourist.Domain;
using Tourist.ShipmentMobile.Infrastructure;

namespace Tourist.ShipmentMobile.ViewModels;
public class AddEditItemViewModel : BaseViewModel, IQueryAttributable
{
    readonly ShipmentsDatabase _dataRepository;
    public AddEditItemViewModel(ShipmentsDatabase dataRepository)
    {
        _dataRepository = dataRepository;
        SaveItemCommand = new Command(
            execute: async () =>
            {
                if (IsNew)
                {
                    Item item = new Item()
                    {
                        Id = int.Parse(Id),
                        Name = Name
                    };
                    await dataRepository.SaveItemAsync(item);
                    IsNew = false;
                }
                else
                {
                    var item = await dataRepository.GetItemAsync(itemId);
                    item.Name = Name;
                    await dataRepository.UpdateItemAsync(item);
                }

                await Shell.Current.GoToAsync("//mainpage//adminmenu//manageitems");
            });
    }

    private int itemId;

    public ICommand SaveItemCommand { private set; get; }

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
        itemId = int.Parse(query["itemId"].ToString());

        if (itemId == -1)
        {
            IsNew = true;
        }
        else
        {
            var item = await _dataRepository.GetItemAsync(itemId);
            Id = item.Id.ToString();
            Name = item.Name;
        }
    }
}
