namespace Backend.Infrastructure.Interfaces.Common;

public interface IJsonAttribute
{
    object TryConvert(string modelValue, Type targertType, out bool success);
}