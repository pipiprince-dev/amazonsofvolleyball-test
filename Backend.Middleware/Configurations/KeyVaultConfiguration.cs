namespace Backend.Middleware.Configurations;

public static class ConfigureKeyVault
{
    public static void ConfigureAppSettings(this WebApplicationBuilder builder)
    {
        builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());

        builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.AddJsonFile("appsettings.json", true, true)
                 .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
        });
    }
}
