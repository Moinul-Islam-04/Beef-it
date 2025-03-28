using Beef__it.Database;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace Beef__it
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
            builder.Services.AddSingleton<ConnectionFactory>();
            builder.Services.AddSingleton<CalendarRepository>();// Makes it so Calendar SQLite database is only created once
            builder.Services.AddTransient<CalendarPage>();//Allows for Dependency Injection (Create CalendarPage with an already created Repository)
            return builder.Build();
        }
    }
}