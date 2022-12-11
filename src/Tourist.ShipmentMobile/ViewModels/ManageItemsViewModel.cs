using System.Collections.ObjectModel;
using Tourist.ShipmentMobile.Infrastructure;
using Tourist.Domain;
using System.ComponentModel;
using System.Windows.Input;

namespace Tourist.ShipmentMobile.ViewModels;
public class ManageItemsViewModel : BaseViewModel
{
	readonly ShipmentsDatabase _dataRepository;

    public ManageItemsViewModel(ShipmentsDatabase dataRepository)
    {
        _dataRepository = dataRepository;
        ItemSelectedCommand = new Command<Item>(
            execute: (a) =>
            {
                Shell.Current.GoToAsync($"//mainpage/adminmenu/manageitems/addedititem?itemId={a.Id}");
            });

        NewItemCommand = new Command(
            execute: () =>
            {
                Shell.Current.GoToAsync($"//mainpage/adminmenu/manageitems/addedititem?itemId=-1");
            });
        LoadData();
    }

    private ObservableCollection<Item> _items;
    public ObservableCollection<Item> Items
    {
        get { return _items; }
        set
        {
            _items = value;
            OnPropertyChanged("Items");
        }
    }

    public ICommand NewItemCommand { private set; get; }

    public ICommand ItemSelectedCommand { private set; get; }

    public async void LoadData()
    {
        var result = await _dataRepository.GetItemsAsync();

        Items = new ObservableCollection<Item>(result);
    }
}
