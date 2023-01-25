namespace Backend.Models;

public class JWTokenModel
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}
