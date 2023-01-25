namespace Backend.Infrastructure.Interfaces.Services;

public interface IPlayerService
{
    Task CreateAsync(PlayerModel player);
    Task DeleteAsync(int id);
    Task<List<PlayerModel>> GetAllAsync();
    Task<PlayerModel> GetByIdAsync(int id);
    Task<List<PlayerModel>> PageAsync(int page = 0);
    Task UpdateAsync(PlayerModel player);
}
