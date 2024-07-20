using System.ComponentModel;

namespace Cleanex.ClientApp.ViewModels;
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

    private bool _isRefreshing;
    public bool IsRefreshing
    {
        get { return _isRefreshing; }
        set
        {
            _isRefreshing = value;
            OnPropertyChanged("IsRefreshing");
        }
    }
}
