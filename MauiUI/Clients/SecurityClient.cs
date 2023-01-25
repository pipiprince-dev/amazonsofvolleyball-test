namespace MauiUI.Clients;

public class SecurityClient : BaseClient, ISecurityClient
{
    public SecurityClient(HttpClient httpClient, MobileAppSettings settings) : base(httpClient, settings)
    {
    }

    public async Task<JWTokenModel> LoginAsync(LoginModel requestParam)
    {
        return await SendPostRequest<JWTokenModel>(EndPoints.Security.LoginAsync, requestParam);
    }

    public async Task<bool> RegisterAsync(RegisterModel requestParam)
    {
        return await SendPostRequest(EndPoints.Security.RegisterAsync, requestParam);
    }
}
