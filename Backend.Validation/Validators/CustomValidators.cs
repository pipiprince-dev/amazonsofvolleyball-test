namespace Backend.Validation.Validators;

public static class CustomValidators
{
    public static bool ValidateUri(string uri)
    {
        return Uri.TryCreate(uri, UriKind.Absolute, out _);
    }
}
