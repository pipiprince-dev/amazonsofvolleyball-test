namespace System.Text.Json;

public static class JsonSerializerHelper
{
    /// <summary>
    /// Returns the configured JsonSerializerOptions wihout any converter
    /// </summary>
    /// <returns></returns>
    public static JsonSerializerOptions GetSerializerOptions()
    {
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
        };

        return options;
    }

    public static JsonSerializerOptions AddJsonSerializerOptions<T>(this JsonSerializerOptions options, JsonConverter<T> converter)
    {
        options.Converters.Add(converter);

        return options;
    }
}
