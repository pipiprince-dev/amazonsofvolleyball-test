namespace Backend.Middleware.HealthStatuses;

public class HealthCheckAPI : IHealthCheck
{
    private HttpClient client { get; }

    public HealthCheckAPI(HttpClient client)
    {
        this.client = client;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            HttpResponseMessage response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, client.BaseAddress));
            response.EnsureSuccessStatusCode();

            return HealthCheckResult.Healthy();
        }
        catch
        {
            return HealthCheckResult.Unhealthy();
        }
    }
}
