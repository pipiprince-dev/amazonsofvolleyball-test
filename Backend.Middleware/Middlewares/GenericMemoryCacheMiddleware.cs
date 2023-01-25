namespace Backend.Middleware.Middlewares;

public class GenericMemoryCacheMiddleware
{
    private readonly GenericMemoryCacheOptions options;
    private readonly RequestDelegate next;
    private readonly IMemoryCache cache;
    private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

    public GenericMemoryCacheMiddleware(RequestDelegate next, GenericMemoryCacheOptions options, IMemoryCache cache)
    {
        this.next = next;
        this.options = options;
        this.cache = cache;
    }

    public async Task InvokeAsync(HttpContext context, IMemoryCache memoryCache)
    {
        if (context.Request is null || !context.Request.Path.HasValue)
        {
            await next(context);
            return;
        }

        var endpointName = context.Request.Path.Value.Trim('/');
        if (endpointName.ToLower() != options.TargetPath)
        {
            await next(context);
            return;
        }

        if (cache.TryGetValue(endpointName, out string responseBody))
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            await context.Response.WriteAsync(responseBody);
            return;
        }

        var originalBody = context.Response.Body;

        try
        {
            await semaphoreSlim.WaitAsync();

            if (cache.TryGetValue(endpointName, out responseBody))
            {
                context.Response.ContentType = "application/json; charset=utf-8";
                await context.Response.WriteAsync(responseBody);
                return;
            }

            using var memStream = new MemoryStream();
            context.Response.Body = memStream;

            await next(context);

            memStream.Position = 0;
            using var reader = new StreamReader(memStream);
            responseBody = await reader.ReadToEndAsync();

            cache.Set(endpointName, responseBody, DateTime.Now.AddMinutes(options.DefaultExpirationInMinutes));

            memStream.Position = 0;
            await memStream.CopyToAsync(originalBody);
        }
        finally
        {
            context.Response.Body = originalBody;
            semaphoreSlim.Release();
        }
    }
}
