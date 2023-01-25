namespace Backend.Models.Common;

public class ApiHealthStatus
{
    [JsonPropertyName("status")]
    public HealthStatus Status { get; set; }

    [JsonPropertyName("results")]
    public IEnumerable<HealthStatusResult> Results { get; set; } = new List<HealthStatusResult>();

    public ApiHealthStatus()
    {
    }

    public ApiHealthStatus(HealthStatus status, IEnumerable<HealthStatusResult> results)
    {
        Status = status;
        Results = results;
    }
}

public class HealthStatusResult
{
    [JsonPropertyName("key")]
    public string Key { get; set; }

    [JsonPropertyName("status")]
    public HealthReportEntry Status { get; set; }

    public HealthStatusResult(string key, HealthReportEntry status)
    {
        Key = key;
        Status = status;
    }
}
