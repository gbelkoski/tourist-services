using System.Collections.ObjectModel;
using Cleanex.ClientApp.Infrastructure;
using Tourist.Domain;
using System.ComponentModel;
using System.Windows.Input;

namespace Cleanex.ClientApp.ViewModels;
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

        RefreshItemsCommand = new Command(
            execute: () =>
            {
                LoadData();
            });

        Items = new ObservableCollection<Item>();
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

    public ICommand RefreshItemsCommand { private set; get; }

    public async void LoadData()
    {
        IsRefreshing = true;
        Items.Clear();
        var result = await _dataRepository.GetItemsAsync();
        result.ForEach(r => Items.Add(r));
        IsRefreshing = false;
    }
}
