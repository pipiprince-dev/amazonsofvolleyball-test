namespace Backend.Middleware.Configurations;

public static class SwaggerConfiguration
{
    public static void ConfigureSwagger(this WebApplicationBuilder builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        var appSettings = builder.Configuration.GetObjectFromConfigSection<AppSettings>("AppSettings");


        builder.Services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc(appSettings.SwaggerPublicGroupAPI, new OpenApiInfo { Title = "http:\\aov.org", Version = appSettings.SwaggerPublicGroupAPI });
            x.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"]}");

            // This code allow you to use XML-comments
            string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                x.IncludeXmlComments(xmlPath);
            }

            x.AddSecurityDefinition
            (
                "Bearer",
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token into the field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                }
            );
        });
    }

    public static void ConfigureSwagger(this WebApplication webApp)
    {
        if (webApp is null)
            return;

        var appSettings = webApp.Configuration.GetObjectFromConfigSection<AppSettings>("AppSettings");

        // Enable middleware to serve generated Swagger as a JSON endpoint.
        webApp.UseSwagger();

        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
        // specifying the Swagger JSON endpoint.
        webApp.UseSwaggerUI(x =>
        {
            x.SwaggerEndpoint($"/swagger/{appSettings.SwaggerPublicGroupAPI}/swagger.json", "aov.org public API");
        });
    }
}
