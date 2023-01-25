namespace MauiUI.Clients;

public class PlayerClient : BaseClient, IPlayerClient
{
    public PlayerClient(HttpClient httpClient, MobileAppSettings settings) : base(httpClient, settings)
    {}

    public async Task<ObservableCollection<PlayerModel>> GetAllAsync()
    {
        var players = await SendGetRequestAsync<List<PlayerModel>>(EndPoints.Player.GetAllAsync);
        return new ObservableCollection<PlayerModel>(players);
    }

    public async Task<bool> CreateAsync(PlayerModel player)
    {
        return await SendPostRequest(EndPoints.Player.CreateAsync, player);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await SendDeleteRequestAsync(EndPoints.Player.DeleteAsync, id);
    }

    public async Task<PlayerModel> GetByIdAsync(int id)
    {
        return await SendGetRequestAsync<PlayerModel>(EndPoints.Player.GetByIdAsync, id);
    }

    public async Task<List<PlayerModel>> PageAsync(int page)
    {
        return await SendGetRequestAsync<List<PlayerModel>>(EndPoints.Player.GetPageAsync, page);
    }

    public async Task<bool> UpdateAsync(PlayerModel player)
    {
        return await SendPutRequest(EndPoints.Player.UpdateAsync, player);
    }
}