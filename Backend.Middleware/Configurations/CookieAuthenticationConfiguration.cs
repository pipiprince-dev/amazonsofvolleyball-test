namespace Backend.Middleware.Configurations;

public static class ConfigureCookieAuthentication
{
    public static void ConfigureApplicationCookieAuthentication(this IServiceCollection services)
    {
        services.Configure<CookieAuthenticationOptions>(options =>
        {
            options.Events = new CookieAuthenticationEvents()
            {
                OnRedirectToLogin = ctx =>
                {
                    if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                    {
                        ctx.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                        return Task.FromResult<object>(null);
                    }
                    else
                    {
                        ctx.Response.Redirect(ctx.RedirectUri);
                        return Task.FromResult<object>(null);
                    }
                }
            };
        });
    }
}
