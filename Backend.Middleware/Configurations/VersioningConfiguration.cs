namespace Backend.Middleware.Configurations;

public static class VersioningConfiguration
{
    public static void ConfigureApplicationVersioning(this WebApplicationBuilder builder)
    {
        var appSettings = builder.Configuration.GetObjectFromConfigSection<AppSettings>("AppSettings");

        builder.Services.AddApiVersioning(config => {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
            config.ApiVersionReader = new QueryStringApiVersionReader("version");
        });

        builder.Services.AddVersionedApiExplorer(opt =>
        {
            opt.GroupNameFormat = "'v'VVV";

            // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
            // can also be used to control the format of the API version in route templates
            opt.SubstituteApiVersionInUrl = true;
        });
    }
}

/*
DECORATE CONTROLLERS LIKE THIS

[Route("api/v{version:apiVersion}/cars")]
[ApiVersion("1.0")]
*/
