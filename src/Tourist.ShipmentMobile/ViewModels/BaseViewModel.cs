using System.ComponentModel;

namespace Tourist.ShipmentMobile.ViewModels;
public abstract class BaseViewModel : INotifyPropertyChanged
{
    public BaseViewModel()
    {
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
