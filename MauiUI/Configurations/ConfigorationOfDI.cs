using MauiUI.ViewModels;

namespace MauiUI.Configurations;

public static class ConfigorationOfDI
{
    public static void ConfigureDI(this MauiAppBuilder builder)
    {
        //built in services
        builder.Services.AddSingleton<HttpClient>();
        builder.Services.AddMemoryCache();
        builder.Services.AddScoped<IMessenger, WeakReferenceMessenger>();

        //client services
        builder.Services.AddTransient<ISecurityClient, SecurityClient>();
        builder.Services.AddTransient<IPlayerClient, PlayerClient>();
        builder.Services.AddTransient<IPositionClient, PositionClient>();

        //viewModels
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<RegisterViewModel>();

        //pages
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<RegisterPage>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<AddOrUpdatePlayer>();
    }
}
