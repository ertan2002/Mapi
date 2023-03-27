using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Mapi.ViewModel;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Mapi.Models.Settings;
using Mapi.Services.GoogleLocation;
using Mapi.Services.DeviceLocation;
using Mapi.Pages;

namespace Mapi;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
        .UseMauiMaps()
        .ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        }).UseMauiCommunityToolkit();

        // App config
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("Mapi.appsettings.json");

        var configuration = new ConfigurationBuilder()
        .AddJsonStream(stream).Build();
        builder.Configuration.AddConfiguration(configuration);
        IConfigurationSection section = builder.Configuration.GetSection("GoogleApi");

        var settings = new Settings()
        {
            GoogleApiKey = section[Mapi.Models.Constants.GOOGLE_API_KEY],
            GoogleApiUrl = section[Mapi.Models.Constants.GOOGLE_API_URL]
        };


        //DI
        builder.Services.AddSingleton<ISettings>(sp=> settings);
        builder.Services.AddTransient<IGoogleLocationService, GoogleLocationService>();
        builder.Services.AddSingleton<IDeviceGeolocation, DeviceGeolocation>();
        builder.Services.AddTransientWithShellRoute<MainPage, MainViewModel>(nameof(MainPage));
        
        return builder.Build();
    }
}