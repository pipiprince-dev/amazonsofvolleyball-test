namespace Backend.Infrastructure.Interfaces.Clients;

public interface IPositionClient
{
    Task<List<PositionModel>> GetAllAsync();
}
