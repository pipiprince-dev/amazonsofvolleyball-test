namespace MauiUI.Configurations;

public static class ConfigurationOfLoadSettings
{
    public static void ConfigurationSettings(this MauiAppBuilder builder)
    {
#if DEBUG
        var file = "settings.Development.json";
#else
        var file = "settings.Production.json";
#endif

        using var stream = FileSystem.OpenAppPackageFileAsync(file).Result;

        var config = new ConfigurationBuilder().AddJsonStream(stream)
                                               .Build();

        builder.Configuration.AddConfiguration(config);
    }
}
