namespace MauiUI.Pages;

[QueryProperty(nameof(Player), "player")]
public partial class PlayerDetailsPage : ContentPage
{
    private PlayerModel player;
    public PlayerModel Player
    {
        get => player;
        set
        {
            player = value;
            OnPropertyChanged();
        }
    }

    public PlayerDetailsPage()
	{
		InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        BindingContext = player;
    }

    private async void OnUpdate(object sender, EventArgs e)
    {
        var navigationParameter = new Dictionary<string, object>
        {
            { "viewModel", Player}
        };

        await Shell.Current.GoToAsync(PageRoutes.AddOrUpdatePage, navigationParameter);
    }
}