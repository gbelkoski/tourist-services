using System;
using System.Collections.ObjectModel;
using System.Text.Encodings.Web;
using System.Windows.Input;
using Tourist.ShipmentMobile.Infrastructure;
using Tourist.ShipmentMobile.Models;

namespace Tourist.ShipmentMobile.ViewModels;
public class ShipmentDetailsViewModel : BaseViewModel, IQueryAttributable
{
    readonly ShipmentsDatabase _dataRepository;
    public ShipmentDetailsViewModel(ShipmentsDatabase dataRepository)
	{
        _dataRepository = dataRepository;
        BarcodeEnteredCommand = new Command(
            execute: async () =>
            {
                if(Barcode.Length != 13)
                {
                    await Application.Current.MainPage.DisplayAlert("Грешка", "Невалиден формат на баркод.", "OK");
                    return;
                }
                var itemCode = GetItemCode(Barcode);
                var weight = GetWeight(Barcode);

                var item = await _dataRepository.GetItemAsync(itemCode);
                if(item == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Грешка", "Артиклот/услугата не постои.", "OK");
                    return;
                }

                var customer = await _dataRepository.GetCustomerAsync(SelectedCustomerId);
                if(customer == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Грешка", "Клиентот не постои.", "OK");
                    return;
                }

                var newLineItemId = await _dataRepository.SaveShipmentLineItemAsync(new Domain.ShipmentLineItem()
                {
                    Barcode = Barcode,
                    ShipmentNo = ShipmentNo,
                    CustomerId = SelectedCustomerId,
                    ItemId = item.Id,
                    DateCreated = DateTime.Now,
                    Weight = weight,
                    IsDirty = true
                });
                ShipmentItems.Add(new ShipmentItemModel()
                {
                    Id = newLineItemId,
                    DateCreated = DateTime.Now,
                    ItemName = item.Name,
                    Weight = weight
                });
                Barcode = string.Empty;
            });
        
        AddLineItemCommand = new Command(
            execute: async () => 
            {
                await Shell.Current.GoToAsync($"//mainpage//shipmentdetails//addshipmentlineitem?shipmentNo={ShipmentNo}&customerId={SelectedCustomerId}");
            });

        DeleteLineItemCommand = new Command(
            execute: async (li) =>
            {
                if((await Shell.Current.DisplayActionSheet("Бришење", "Откажи", "Да", "Дали сте сигурни?"))=="Да")
                {
                    var lineItemModel = li as ShipmentItemModel;
                    var lineItemDb = await _dataRepository.GetShipmentLineItemAsync(lineItemModel.Id);
                    if(lineItemDb.DateShipped != null)
                    {
                        await Shell.Current.DisplayAlert("Грешка", "Ставката е веќе испорачана", "ОК");
                        return;
                    }
                    await _dataRepository.DeleteShipmentLineItemAsync(lineItemDb);
                    ShipmentItems.Remove(lineItemModel);
                }
            });

        PrintShipmentCommand = new Command(
            execute: async () =>
            {
            ShipmentHtml = @$"<!DOCTYPE html>
                            <html>
	                            <head>
		                            <meta charset=""utf-8"" />
		                            <title>Cleanex - испратница</title>

		                            <style>
			                            .invoice-box {{
				                            max-width: 800px;
				                            margin: auto;
				                            padding: 30px;
				                            border: 1px solid #eee;
				                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.15);
				                            font-size: 16px;
				                            line-height: 24px;
				                            font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;
				                            color: #555;
			                            }}

			                            .invoice-box table {{
				                            width: 100%;
				                            line-height: inherit;
				                            text-align: left;
			                            }}

			                            .invoice-box table td {{
				                            padding: 5px;
				                            vertical-align: top;
			                            }}

			                            .invoice-box table tr td:nth-child(2) {{
				                            text-align: right;
			                            }}

			                            .invoice-box table tr.top table td {{
				                            padding-bottom: 20px;
			                            }}

			                            .invoice-box table tr.top table td.title {{
				                            font-size: 45px;
				                            line-height: 45px;
				                            color: #333;
			                            }}

			                            .invoice-box table tr.information table td {{
				                            padding-bottom: 40px;
			                            }}

			                            .invoice-box table tr.heading td {{
				                            background: #eee;
				                            border-bottom: 1px solid #ddd;
				                            font-weight: bold;
			                            }}

			                            .invoice-box table tr.details td {{
				                            padding-bottom: 20px;
			                            }}

			                            .invoice-box table tr.item td {{
				                            border-bottom: 1px solid #eee;
			                            }}

			                            .invoice-box table tr.item.last td {{
				                            border-bottom: none;
			                            }}

			                            .invoice-box table tr.total td:nth-child(2) {{
				                            border-top: 2px solid #eee;
				                            font-weight: bold;
			                            }}

			                            @media only screen and (max-width: 600px) {{
				                            .invoice-box table tr.top table td {{
					                            width: 100%;
					                            display: block;
					                            text-align: center;
				                            }}

				                            .invoice-box table tr.information table td {{
					                            width: 100%;
					                            display: block;
					                            text-align: center;
				                            }}
			                            }}

			                            /** RTL **/
			                            .invoice-box.rtl {{
				                            direction: rtl;
				                            font-family: Tahoma, 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;
			                            }}

			                            .invoice-box.rtl table {{
				                            text-align: right;
			                            }}

			                            .invoice-box.rtl table tr td:nth-child(2) {{
				                            text-align: left;
			                            }}
		                            </style>
	                            </head>

	                            <body>
		                            <div class=""invoice-box"">
			                            <table cellpadding=""0"" cellspacing=""0"">
				                            <tr c
lass=""top"">
					                            <td colspan=""2"">
						                            <table>
							                            <tr>
								                            <td class=""title"">
									                            <img
										                            src=""https://www.cleanex.mk/images/logo.png""
										                            style=""width: 100%; max-width: 300px""
									                            />
								                            </td>

								                            <td>
									                            Испратница бр #: {ShipmentNo}<br />
									                            Датум: {DateTime.Now.Date.ToShortDateString()}
								                            </td>
							                            </tr>
						                            </table>
					                            </td>
				                            </tr>

				                            <tr class=""information"">
					                            <td colspan=""2"">
						                            <table>
							                            <tr>
								                            <td>
									                            CLEANEX PROFESSIONAL - СЕРВИС ЗА ХИГИЕНА<br />
									                            Ул. Железничка бр. 122<br />
									                            1000 Охрид
								                            </td>

								                            <td>
									                            {SelectedCustomerName}<br />
								                            </td>
							                            </tr>
						                            </table>
					                            </td>
				                            </tr>

				                            <tr class=""heading"">
					                            <td>Артикл/услуга</td>

					                            <td>Количина</td>
				                            </tr>";

                foreach(var item in ShipmentItems)
                {
                    ShipmentHtml += $@"<tr class=""item"">
					                        <td>{item.ItemName}</td>
					                        <td>{item.Weight.ToString("N2")}</td>
				                        </tr>";
                }

				ShipmentHtml += $@"<tr class=""total"">
					                    <td></td>

					                    <td>Вкупно: {ShipmentItems.Sum(i => i.Weight).ToString("N2")}</td>
				                    </tr>
			                    </table>
		                    </div>
	                    </body>
                    </html>";
				ShipmentItems.Clear();
				await Shell.Current.GoToAsync("//mainpage//customerpicker");
			});
    }

    public ICommand BarcodeEnteredCommand { private set; get; }

    public ICommand AddLineItemCommand { private set; get; }

    public ICommand DeleteLineItemCommand { private set; get; }

    public ICommand PrintShipmentCommand { private set; get; }

    ObservableCollection<ShipmentItemModel> _shipmentItems;
    public ObservableCollection<ShipmentItemModel> ShipmentItems
    {
        get { return _shipmentItems; }
        set
        {
            _shipmentItems = value;
            OnPropertyChanged("ShipmentItems");
        }
    }

    private string _barcode;
    public string Barcode
    {
        get { return _barcode; }
        set
        {
            _barcode = value;
            OnPropertyChanged("Barcode");
        }
    }

    public int SelectedCustomerId { get; set; }

    private string _selectedCustomerName;
    public string SelectedCustomerName
    {
        get { return _selectedCustomerName; }
        set
        {
            _selectedCustomerName = value;
            OnPropertyChanged("SelectedCustomerName");
        }
    }
    private string _shipmentNo;
    public string ShipmentNo
    {
        get { return _shipmentNo; }
        set
        {
            _shipmentNo = value;
            OnPropertyChanged("ShipmentNo");
        }
    }

    private string _shipmentHtml;
    public string ShipmentHtml
    {
        get { return _shipmentHtml; }
        set
        {
            _shipmentHtml = value;
            OnPropertyChanged($"{nameof(ShipmentHtml)}");
        }
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        SelectedCustomerId = int.Parse(query["customerId"].ToString());
        SelectedCustomerName = (await _dataRepository.GetCustomerAsync(SelectedCustomerId)).Name;

        var activeShipmentItems = await _dataRepository.GetActiveShipmentAsync(SelectedCustomerId);
        ShipmentItems = new ObservableCollection<ShipmentItemModel>();
        if (activeShipmentItems.Any())
        {
            activeShipmentItems.ForEach(async s => ShipmentItems.Add(
                new ShipmentItemModel()
                {
                    Id = s.Id,
                    DateCreated = s.DateCreated,
                    ItemName = (await _dataRepository.GetItemAsync(s.ItemId)).Name,
                    Weight = s.Weight
                }));
            ShipmentNo = activeShipmentItems.FirstOrDefault().ShipmentNo;
        }
        else
        {
            var last =  await _dataRepository.GetLatestShipedItemAsync(SelectedCustomerId);
            if(last == null)
            {
                ShipmentNo = "1";
            }
            else if(last.DateShipped.Value.Year == DateTime.Now.Year)
            {
                ShipmentNo = (int.Parse(last.ShipmentNo) + 1).ToString();
            }
            else if(last.DateShipped.Value.Year < DateTime.Now.Year)
            {
                ShipmentNo = "1";
            }
        }
    }

    private int GetCustomerId(string barcode)
    {
        string customerString = barcode.Substring(0, 6);
        return int.Parse(customerString);
    }

    private string GetItemCode(string barcode)
    {
        string itemString = barcode.Substring(6, 2);
        return itemString;
    }

    private decimal GetWeight(string barcode)
    {
        var weightString = barcode.Substring(8, 4);
        decimal weigth = decimal.Parse(weightString.Substring(0, 2)) + decimal.Parse(weightString.Substring(2, 2)) / 100;
        return weigth;
    }
}
