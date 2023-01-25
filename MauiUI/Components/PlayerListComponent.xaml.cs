namespace MauiUI.Components;

public partial class PlayerListComponent : ContentView
{
    public static readonly BindableProperty PlayerProperty = BindableProperty.Create(nameof(Player), typeof(PlayerModel), typeof(PlayerListComponent), null);

    public PlayerModel Player
    {
        get => (PlayerModel)GetValue(PlayerProperty);
        set => SetValue(PlayerProperty, value);
    }

    public PlayerListComponent()
	{
		InitializeComponent();
    }

    private async void OnTappHandler(object sender, TappedEventArgs args)
    {
        var navigationParameter = new Dictionary<string, object>
        {
            { "player", Player }
        };

        await Shell.Current.GoToAsync(PageRoutes.DetailsPage, true, navigationParameter);
    }

    private void OnDeleteEventHandler(object sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send<DeletePlayerMessage>(new DeletePlayerMessage
        { 
            Id = Player.Id
        });
    }
}