using System.Globalization;
using System.Windows.Input;
using Tourist.Domain;
using Cleanex.ClientApp.Infrastructure;

namespace Cleanex.ClientApp.ViewModels;
public class AddShipmentLineItemViewModel : BaseViewModel, IQueryAttributable
{
    readonly ShipmentsDatabase _dataRepository;
    public AddShipmentLineItemViewModel(ShipmentsDatabase dataRepository)
    {
        _dataRepository = dataRepository;

		OKCommand = new Command(
            execute: async () =>
            {
                var item = await dataRepository.GetItemAsync(ItemCode);
                if (item == null)
                {
                    await Shell.Current.DisplayAlert("Грешка", "Артиклот/услугата не постои", "ОК");
                }
                else if(string.IsNullOrEmpty(Weight) || !decimal.TryParse(Weight, CultureInfo.InvariantCulture, out decimal WeightDecimal))
                {
                    await Shell.Current.DisplayAlert("Грешка", "Тежината не е валидна", "ОК");
                }
                else
                {
                    var newLineItemId = await _dataRepository.SaveShipmentLineItemAsync(new ShipmentLineItem()
                    {
                        ShipmentNo = shipmentNo,
                        CustomerId = selectedCustomerId,
                        ItemId = item.Id,
                        DateCreated = DateTime.Now,
                        Weight = WeightDecimal,
                        IsDirty = true
                    });
                    await Shell.Current.Navigation.PopAsync();
                }
            });
    }

    public ICommand OKCommand { private set; get; }

    string shipmentNo;
    int selectedCustomerId;

    private string _itemCode;
    public string ItemCode
    {
        get { return _itemCode; }
        set
        {
            _itemCode = value;
            OnPropertyChanged("ItemCode");
        }
    }

	private string _weight;
    public string Weight
    {
        get { return _weight; }
        set
        {
            _weight = value;
            OnPropertyChanged("Weight");
        }
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        selectedCustomerId = int.Parse(query["customerId"].ToString());
        shipmentNo = query["shipmentNo"].ToString();
    }
}