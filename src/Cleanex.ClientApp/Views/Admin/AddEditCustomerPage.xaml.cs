using System.Collections.ObjectModel;
using System.ComponentModel;
using Cleanex.ClientApp.ViewModels;

namespace Cleanex.ClientApp;
public partial class AddEditCustomerPage : ContentPage
{
    public AddEditCustomerPage(AddEditCustomerViewModel viewModel)
	{
        BindingContext = viewModel;
		InitializeComponent();
    }
}
