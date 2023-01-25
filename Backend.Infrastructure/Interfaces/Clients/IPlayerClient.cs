using System.Collections.ObjectModel;

namespace Backend.Infrastructure.Interfaces.Clients;

public interface IPlayerClient
{
    Task<bool> CreateAsync(PlayerModel player);
    Task<bool> DeleteAsync(int id);
    Task<ObservableCollection<PlayerModel>> GetAllAsync();
    Task<PlayerModel> GetByIdAsync(int id);
    Task<List<PlayerModel>> PageAsync(int page = 0);
    Task<bool> UpdateAsync(PlayerModel player);
}
