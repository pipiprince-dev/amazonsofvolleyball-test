namespace Backend.Middleware.Configurations;

public static class ConfigureCors
{
    public static void AddApplicationCors(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddCors(options =>
        {
#if DEBUG
            options.AddPolicy("Debug", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
#else
            string[] origins = BuildOrigins();
            options.AddPolicy("Production", builder =>
            {
                builder.WithOrigins(origins)
                       .SetIsOriginAllowedToAllowWildcardSubdomains()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
#endif
        });
    }

    public static void AddApplicationCors(this IApplicationBuilder applicationBuilder)
    {
#if DEBUG
        applicationBuilder.UseCors("Debug");
#else
        applicationBuilder.UseCors("Production");
#endif
    }

    private static string[] BuildOrigins()
    {
        string portalOrigin = "mywebsite.com";
        string localhost3000 = "localhost:3000";

        List<string> origins = new List<string>()
        {
            $"https://{portalOrigin}", $"http://{portalOrigin}", $"https://*.{portalOrigin}", $"http://*.{portalOrigin}",
            $"https://{localhost3000}", $"http://{localhost3000}", $"https://*.{localhost3000}", $"http://*.{localhost3000}"
        };

        return origins.ToArray();
    }
}
