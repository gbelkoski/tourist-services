using Microsoft.Extensions.Logging;
using Tourist.ShipmentMobile.Infrastructure;

namespace Tourist.ShipmentMobile;
public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.RegisterServices()
			.RegisterViewModels()
            .RegisterViews();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<ShipmentsDatabase>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
	{
		mauiAppBuilder.Services.AddTransient<ViewModels.CustomerPickerViewModel>();
		mauiAppBuilder.Services.AddTransient<ViewModels.ShipmentDetailsViewModel>();
		mauiAppBuilder.Services.AddTransient<ViewModels.ShipmentsDeliveredViewModel>();

		return mauiAppBuilder;
	}

	public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
	{
		mauiAppBuilder.Services.AddTransient<CustomerPickerPage>();
		mauiAppBuilder.Services.AddTransient<ShipmentDetailsPage>();
        mauiAppBuilder.Services.AddTransient<ShipmentsDeliveredPage>();

		return mauiAppBuilder;
	}
}
