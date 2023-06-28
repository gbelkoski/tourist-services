using Tourist.ShipmentMobile.Jobs;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace Tourist.ShipmentMobile;

public partial class App : Microsoft.Maui.Controls.Application
{
	public App(SyncDataJob syncDataJob)
	{
		InitializeComponent();

		MainPage = new AppShell();

		syncDataJob.Schedule();
	}

    protected override void OnStart()
    {
        base.OnStart();

        AppCenter.Start("android=b1d19065-5403-4d7e-9672-0399ba4b8315;" +
                  "uwp={Your UWP App secret here};" +
                  "ios={Your iOS App secret here};" +
                  "macos={Your macOS App secret here};",
                  typeof(Analytics), typeof(Crashes));
    }
}
