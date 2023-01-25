namespace Backend.Infrastructure.Interfaces.Services;

public interface ISecurityService
{
    Task<JWTokenModel> LoginAsync(LoginModel model);
    Task RegisterAdminAsync(RegisterModel model);
    Task RegisterAsync(RegisterModel model);
}
