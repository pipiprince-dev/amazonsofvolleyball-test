namespace Backend.Middleware.Configurations;

public static class ConfigureOptionsVerb
{
    public static IApplicationBuilder AddApplicationOptionsVerbHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<OptionsVerbMiddleware>();
    }
}
