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

		//builder.Services
		//.Init()
		//.AddCommandHandlers()
		//.AddQueryHandlers()
		//.AddInMemoryCommandDispatcher()
		//.AddInMemoryQueryDispatcher();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<ShipmentsDatabase>();
        //mauiAppBuilder.Services.AddSingleton(new DatabaseConfig { ConnectionString = "Data Source=Tourist.db" });
        //mauiAppBuilder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();
        //mauiAppBuilder.Services.AddTransient<ICustomerRepository,CustomerRepository>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
	{
		//mauiAppBuilder.Services.AddSingleton<ViewModels.MainViewModel>();

		mauiAppBuilder.Services.AddTransient<ViewModels.CustomerPickerViewModel>();
		

		return mauiAppBuilder;
	}

	public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
	{
		mauiAppBuilder.Services.AddTransient<CustomerPickerPage>();

		return mauiAppBuilder;
	}
}
