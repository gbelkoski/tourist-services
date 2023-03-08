using Tourist.ShipmentMobile.Jobs;

namespace Tourist.ShipmentMobile;

public partial class App : Microsoft.Maui.Controls.Application
{
	public App(SyncDataJob syncDataJob)
	{
		InitializeComponent();

		MainPage = new AppShell();

		syncDataJob.Schedule();
	}
}
