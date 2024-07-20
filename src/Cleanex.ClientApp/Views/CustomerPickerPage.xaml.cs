using System.Collections.ObjectModel;
using System.ComponentModel;
using Cleanex.ClientApp.ViewModels;

namespace Cleanex.ClientApp;
public partial class CustomerPickerPage : ContentPage
{
    public CustomerPickerPage(CustomerPickerViewModel viewModel)
	{
		BindingContext = viewModel;

		InitializeComponent();
    }
}
