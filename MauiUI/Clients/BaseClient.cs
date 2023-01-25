using Microsoft.Extensions.Logging;

namespace MauiUI.Clients;

public abstract class BaseClient
{
    private HttpClient httpClient;
    private readonly MobileAppSettings settings;

    private string BaseURL
    {
        get
        {
            return DeviceInfo.Platform == DevicePlatform.Android ?
                                          this.settings.AndroidBaseURL : 
                                          this.settings.IosBaseURL;
        }
    }

    protected BaseClient(HttpClient httpClient, MobileAppSettings settings)
    {
        this.settings = settings;

        Task.Run(async () => await InitializeAsync(httpClient));
    }

    private async Task InitializeAsync(HttpClient httpClient)
    {
        this.httpClient = await BuildHttpClient(httpClient);
    }

    /// <summary>
    /// Creates a simple get request
    /// </summary>
    /// <typeparam name="T">The return type</typeparam>
    /// <param name="route">The route part without the base url</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    protected async Task<T> SendGetRequestAsync<T>(string route)
    {
        try
        {
            var uri = BuildUri(route);

            var response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var content = await SerializeResponse<T>(response.Content);
            return content;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Creates a simple get request with a url parameter
    /// </summary>
    /// <typeparam name="T">The return type</typeparam>
    /// <param name="route">The route part without the base url</param>
    /// <param name="routParam">Rout parameter</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    protected async Task<T> SendGetRequestAsync<T>(string route, object routParam)
    {
        try
        {
            var uri = BuildUri(route, routParam);

            var response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var content = await SerializeResponse<T>(response.Content);
            return content;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Creates a simple delete request
    /// </summary>
    /// <param name="route">The route part without the base url</param>
    /// <param name="routParam">Rout parameter</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    protected async Task<bool> SendDeleteRequestAsync(string route, object routParam)
    {
        try
        {
            var uri = BuildUri(route, routParam);

            var response = await httpClient.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();

            return true;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    /// <summary>
    /// Sends a POST request to the specified route, containing the body serialized as a JSON in the request body
    /// </summary>
    /// <param name="route">The route part without the base url</param>
    /// <param name="body">The request body object</param>
    /// <returns></returns>
    protected async Task<bool> SendPostRequest(string route, object body)
    {
        try
        {
            var request = BuildRequestMessage(HttpMethod.Post, route, body);

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return true;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    /// <summary>
    /// Sends a POST request to the specified route, containing the body serialized as a JSON in the request body
    /// </summary>
    /// <param name="route">The route part without the base url</param>
    /// <param name="body">The request body object</param>
    /// <returns></returns>
    protected async Task<TResponse> SendPostRequest<TResponse>(string route, object body)
    {
        try
        {
            var request = BuildRequestMessage(HttpMethod.Post, route, body);

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await SerializeResponse<TResponse>(response.Content);
            return content;

        }
        catch (Exception ex)
        {
            return (TResponse)default;
        }
    }

    /// <summary>
    /// Sends a PUT request to the specified route, containing the body serialized as a JSON in the request body
    /// </summary>
    /// <param name="route">The route part without the base url</param>
    /// <param name="body">The request body object</param>
    /// <returns></returns>
    protected async Task<bool> SendPutRequest(string route, object body)
    {
        try
        {
            var request = BuildRequestMessage(HttpMethod.Put, route, body);

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return true;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private async Task<HttpClient> BuildHttpClient(HttpClient httpClient)
    {
#if DEBUG
        var handler = new HttpsClientHandlerService();
        httpClient = new HttpClient(handler.GetPlatformMessageHandler());
#endif

        httpClient.BaseAddress = new Uri(BaseURL);

        httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
        httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
        httpClient.DefaultRequestHeaders.Add("Host", "amazonsofvolleyball");

        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));

        var jwt = await StorageService.Secure.GetAsync<JWTokenModel>(StorageKeys.Secure.JWT);
        if(jwt is not null)
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.Token);

        return httpClient;
    }

    private HttpRequestMessage BuildRequestMessage(HttpMethod httpMethod, string route, object body)
    {
        return new HttpRequestMessage
        {
            Method = httpMethod,
            RequestUri = BuildUri(route),
            Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, MediaTypeNames.Application.Json)
        };
    }

    private Uri BuildUri(string route)
    { 
        return new Uri(Path.Combine(BaseURL, settings.ApiVersion, route));
    }

    private Uri BuildUri(string route, object routParam)
    {
        return new Uri(Path.Combine(BaseURL, settings.ApiVersion, route, $"{routParam}"));
    }

    private async Task<T> SerializeResponse<T>(HttpContent content)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        using var stream = await content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<T>(stream, options);
        
        return result;
    }
}

