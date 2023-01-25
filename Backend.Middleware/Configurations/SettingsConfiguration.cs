namespace Backend.Middleware.Configurations;

public static class SettingsConfiguration
{
    public static void ConfigureSettingsMapper(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));
        builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
    }
}
