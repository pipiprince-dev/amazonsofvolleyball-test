namespace MauiUI.Clients;

public class PositionClient : BaseClient, IPositionClient
{
	public PositionClient(HttpClient httpClient, MobileAppSettings settings) : base(httpClient, settings)
	{
	}

	public async Task<List<PositionModel>> GetAllAsync()
	{
		return await SendGetRequestAsync<List<PositionModel>>(EndPoints.Position.GetAllAsync);
	}
}
