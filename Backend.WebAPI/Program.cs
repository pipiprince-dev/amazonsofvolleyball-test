var builder = WebApplication.CreateBuilder(args);

builder.Services.AddContainersForDI();
builder.ConfigureAppSettings();
builder.ConfigureSettingsMapper();
builder.ConfigureEntityFramework();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
                {
                    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                })
                .AddMvcCore(options =>
                {
                    // Add custom binder provider for mapping json object form multipart/form-data
                    options.ModelBinderProviders.Insert(0, new JsonModelBinderProvider());
                    // Add "Cache-Control" header
                    options.Filters.Add(typeof(CacheControlFilter));
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .AddApiExplorer();

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.ConfigureAuthentication();
builder.ConfigureApplicationVersioning();
builder.Services.ConfigureApplicationCookieAuthentication();
builder.Services.AddApplicationCompression();
builder.Services.AddApplicationCors();
builder.Services.AddRazorPages();
builder.Services.AddRouting();
builder.Services.AddControllersWithViews();
builder.Services.AddAppHealthChecks();
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.ConfigureSwagger();
builder.ConfigureSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.AddApplicationCompression();
app.ConfigureSwagger();
app.AddApplicationCors();
app.AddApplicationOptionsVerbHandler(); // Options verb handler must be added after CORS. See: https://github.com/drwatson1/AspNet-Core-REST-Service/wiki#cross-origin-resource-sharing-cors-and-preflight-requests
app.AddAppHealthChecks();
app.UseStaticFiles();
app.UseMiddleware<GenericMemoryCacheMiddleware>(new GenericMemoryCacheOptions
{
    TargetPath = "product/filteroptions",
    DefaultExpirationInMinutes = 1440,
});
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

//Auto migration
using var serviceScope = (app as IApplicationBuilder).ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
using var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
await context?.Database?.MigrateAsync();

if (!context.Players.Any() && app.Environment.IsDevelopment())
    InsertPlayersInDB();

Log.Logger.Information("Web API up and runing.");
Log.Logger.Information("DB up and runing.");
Log.Logger.Information("Server configuration is completed");

app.Run();

async void InsertPlayersInDB()
{
    var players = new List<PlayerEntity>();

    using FileStream fs = new FileStream("./data.json", FileMode.Open, FileAccess.Read, FileShare.None);
    using StreamReader r = new StreamReader(fs, Encoding.UTF8);
    var jsonString = r.ReadToEnd();

    if (string.IsNullOrWhiteSpace(jsonString))
        return;

    players = JsonSerializer.Deserialize<List<PlayerEntity>>(jsonString);

    await context.Players.AddRangeAsync(players);
    await context.SaveChangesAsync();
}