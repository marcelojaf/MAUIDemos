using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace MauiMaps;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiMaps()
			.UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<MainPageViewModel>();
		builder.Services.AddSingleton<MainPage>();

		builder.ConfigureMauiHandlers(handlers =>
		{
			handlers.AddHandler<Microsoft.Maui.Controls.Maps.Map, MauiMaps.CustomMapHandler>();
		});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

