namespace MauiUI;

public partial class MainPage : ContentPage
{
    private readonly IPlayerClient playerClient;
    private readonly IPositionClient positionClient;
    private readonly IMemoryCache memoryCache;

    private ObservableCollection<PlayerModel> players;

    public MainPage(IPlayerClient playerClient, IPositionClient positionClient, IMemoryCache memoryCache)
    {
        InitializeComponent();

        this.playerClient = playerClient;
        this.positionClient = positionClient;
        this.memoryCache = memoryCache;

        SubScribeOnDelte();
    }

    private void SubScribeOnDelte()
    {
        WeakReferenceMessenger.Default.Register<DeletePlayerMessage>(this, async (recepient, messaage) =>
        {
            await OnDelete(messaage);
        });
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        players = await playerClient.GetAllAsync();
        collectionView.ItemsSource = players;

        await LoadCacheData();
    }

    private async Task LoadCacheData()
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromSeconds(3600))
        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
        .SetPriority(CacheItemPriority.Normal)
        .SetSize(1024);

        if (!memoryCache.TryGetValue(CacheKeys.Positions, out List<PositionModel> positions))
        {
            positions = await this.positionClient.GetAllAsync();
            memoryCache.Set(CacheKeys.Positions, positions, cacheEntryOptions);
        }
    }

    private async void OnAddNew(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(PageRoutes.AddOrUpdatePage);
    }

    private async Task OnDelete(DeletePlayerMessage message)
    {
        if (message is null)
            return;

        var player = players.FirstOrDefault(x => x.Id == message.Id);

        if (player is null)
            return;

        await playerClient.DeleteAsync(message.Id);
        
        var index = players.IndexOf(player);
        players.RemoveAt(index);
    }

}