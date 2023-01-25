namespace Backend.Middleware.Configurations;

public static class ConfigureDependencyInjection
{
    public static void AddContainersForDI(this IServiceCollection services)
    {

        services.AddTransient<ISecurityService, SecurityService>();
        services.AddTransient<IPlayerService, PlayerService>();
        services.AddTransient<IPositionService, PositionService>();


        // Add useful interface for accessing the ActionContext outside a controller.
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        // Add useful interface for accessing the HttpContext outside a controller.
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        // Add useful interface for accessing the IUrlHelper outside a controller.
        services.AddScoped<IUrlHelper>(x => x.GetRequiredService<IUrlHelperFactory>()
                                             .GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext));

    }
}
