namespace Backend.Middleware.Configurations;

public static class ConfigureDatabase
{
    public static void ConfigureEntityFramework(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
            options.UseLazyLoadingProxies()
                   .UseSqlServer(connectionString, options =>
                   {
                       options.MigrationsAssembly("Backend.Database");
                       options.EnableRetryOnFailure();
                   })
        );

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();    }
}
