using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RosaryForToday.Configuration;

namespace RosaryForToday.UI
{
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
                });

#if DEBUG
			builder.Logging.AddDebug();
#endif

            builder.Services
                .AddViews()
                .AddRequiredClasses(builder.Configuration);

            return builder.Build();
        }
    }
}
