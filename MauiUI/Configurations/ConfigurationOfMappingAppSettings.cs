namespace MauiUI.Configurations;

public static class ConfigurationOfMappingAppSettings
{
    public static void ConfigureAppSettings(this MauiAppBuilder builder)
    {
        var appSettings = builder.Configuration.GetRequiredSection("MobileAppSettings").Get<MobileAppSettings>();
        builder.Services.AddSingleton(appSettings);
    }
}
