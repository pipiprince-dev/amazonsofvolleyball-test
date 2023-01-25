namespace Backend.Middleware.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class FromJsonAttribute : Attribute, IJsonAttribute
{
    public object TryConvert(string modelValue, Type targetType, out bool success)
    {
        if (modelValue is null || targetType is null)
        {
            success = false;
            return new object();
        }

        var value = JsonSerializer.Deserialize(modelValue, targetType);
        success = value is not null;
        return value;
    }
}
