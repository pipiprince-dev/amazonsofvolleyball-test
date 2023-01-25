namespace Backend.Middleware.Configurations;

public static class ConfigureHealthChecks
{
    public static void AddAppHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
                .AddCheck<HealthCheckAPI>("HealthCheckAPI");
    }

    public static void AddAppHealthChecks(this IApplicationBuilder app)
    {
        app.UseHealthChecks("/api/health", new HealthCheckOptions()
        {
            ResponseWriter = (httpContext, result) =>
            {
                httpContext.Response.ContentType = "application/json";

                var healthStatus = new ApiHealthStatus
                {
                    Status = result.Status,
                    Results = result.Entries.Select(keyPairs => new HealthStatusResult(keyPairs.Key, keyPairs.Value))
                };

                var json = JsonSerializer.Serialize(healthStatus, JsonSerializerHelper.GetSerializerOptions());
                
                return httpContext.Response.WriteAsync(json);
            }
        });
    }
}
