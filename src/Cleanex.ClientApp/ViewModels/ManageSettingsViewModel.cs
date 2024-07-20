using System.Windows.Input;

namespace Cleanex.ClientApp.ViewModels;
public class ManageSettingsViewModel : BaseViewModel
{
    const string ApiBaseUrlKey = "ApiBaseUrl";

    private string _apiBaseUrl;
    public string ApiBaseUrl
    {
        get { return _apiBaseUrl; }
        set
        {
            _apiBaseUrl = value;
            OnPropertyChanged(ApiBaseUrlKey);
        }
    }

    public ManageSettingsViewModel()
    {
        ApiBaseUrl = Preferences.Default.Get(ApiBaseUrlKey, "http://cleanex.kreditinfo.mk");

        SaveCommand = new Command(
        execute: () =>
        {
            Preferences.Set(ApiBaseUrlKey, ApiBaseUrl);
        });
    }

    public ICommand SaveCommand { private set; get; }
}
