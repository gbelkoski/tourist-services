using System.Collections.ObjectModel;
using System.ComponentModel;
using Cleanex.ClientApp.ViewModels;

namespace Cleanex.ClientApp;
public partial class ManageCustomersPage : ContentPage
{
    public ManageCustomersPage(ManageCustomersViewModel viewModel)
	{
        BindingContext = viewModel;
		InitializeComponent();
    }
}
