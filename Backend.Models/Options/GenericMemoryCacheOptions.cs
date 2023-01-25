namespace Backend.Models.Options;

public class GenericMemoryCacheOptions
{
    public string TargetPath { get; set; }
    public int DefaultExpirationInMinutes { get; set; } = 60;
}
