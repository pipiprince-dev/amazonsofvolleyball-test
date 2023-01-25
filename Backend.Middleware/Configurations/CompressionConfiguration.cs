namespace Backend.Middleware.Configurations;

public static class ConfigureCompression
{
    public static void AddApplicationCompression(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddResponseCompression(options =>
        {
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
            /*
             * use if custom compression are needed *
             options.Providers.Add<CustomCompressionProvider>();
            */
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat( new[]
            {
                "image/svg+xml"
            });
        }); ;

        serviceCollection.Configure<BrotliCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Optimal;
        });
    }

    public static void AddApplicationCompression(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseResponseCompression();
    }
}
