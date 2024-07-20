using System.Collections.ObjectModel;
using System.ComponentModel;
using Cleanex.ClientApp.ViewModels;

namespace Cleanex.ClientApp;
public partial class AddEditItemPage : ContentPage
{
    public AddEditItemPage(AddEditItemViewModel viewModel)
	{
        BindingContext = viewModel;
		InitializeComponent();
    }
}
