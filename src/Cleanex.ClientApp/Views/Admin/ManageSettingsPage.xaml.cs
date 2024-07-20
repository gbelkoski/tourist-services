using Cleanex.ClientApp.ViewModels;

namespace Cleanex.ClientApp;
public partial class ManageSettingsPage : ContentPage
{
	public ManageSettingsPage(ManageSettingsViewModel viewModel)
	{
		this.BindingContext = viewModel;
		InitializeComponent();
	}
}
