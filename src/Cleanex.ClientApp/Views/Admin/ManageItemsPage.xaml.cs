using System.Collections.ObjectModel;
using System.ComponentModel;
using Cleanex.ClientApp.ViewModels;

namespace Cleanex.ClientApp;
public partial class ManageItemsPage : ContentPage
{
    public ManageItemsPage(ManageItemsViewModel viewModel)
	{
        BindingContext = viewModel;
		InitializeComponent();
    }

}
