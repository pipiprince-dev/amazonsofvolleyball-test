namespace Backend.Middleware.Configurations;

public static class ConfigureException
{
    public static IApplicationBuilder AddApplicationExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}
