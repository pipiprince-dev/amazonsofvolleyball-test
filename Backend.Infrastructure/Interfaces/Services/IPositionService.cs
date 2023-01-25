namespace Backend.Infrastructure.Interfaces.Services;

public interface IPositionService
{
    Task<List<PositionModel>> GetAllAsync();
}
