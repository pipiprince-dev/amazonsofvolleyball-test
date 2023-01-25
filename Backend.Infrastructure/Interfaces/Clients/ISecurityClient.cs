namespace Backend.Infrastructure.Interfaces.Clients
{
    public interface ISecurityClient
    {
        Task<JWTokenModel> LoginAsync(LoginModel requestParam);
        Task<bool> RegisterAsync(RegisterModel requestParam);
    }
}
