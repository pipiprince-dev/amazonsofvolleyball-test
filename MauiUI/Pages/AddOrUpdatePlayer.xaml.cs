namespace MauiUI.Pages;

[QueryProperty(nameof(ViewModel), "viewModel")]
public partial class AddOrUpdatePlayer : ContentPage
{
    private PlayerModel viewModel;
    public PlayerModel ViewModel
    {
        get => viewModel;
        set
        {
            viewModel = value;
            OnPropertyChanged("viewModel");
        }
    }

    private List<PositionModel> positions = new List<PositionModel>();
    

    private readonly IMemoryCache memoryCache;
    private readonly IPlayerClient playerClient;


    private delegate Task Action();
    private Action asyncAction;


    public AddOrUpdatePlayer(IMemoryCache memoryCache, IPlayerClient playerClient)
    {
        this.memoryCache = memoryCache;
        this.playerClient = playerClient;

        memoryCache.TryGetValue(CacheKeys.Positions, out positions);

        InitializeComponent();
        SetUpControls();

#if ANDROID
        MauiUI.Platforms.Android.KeyboardHelper.HideKeyboard();
#elif IOS
        MauiUI.Platforms.iOS.KeyboardHelper.HideKeyboard();
#endif
    }

    protected override void OnAppearing()
    {
        viewModel ??= new PlayerModel();
        viewModel.ValidationCompleted += OnValidationHandler;

        BindingContext = viewModel;

        SetUpControls();
        SetTitles();
        SetActionPointer();
    }

    private void SetUpControls()
    {
        birthday.Date= DateTime.Now.Date;
        birthday.MinimumDate = new DateTime(1900, 1, 1);
        birthday.MaximumDate = DateTime.Now.Date.AddDays(-1);

        position.ItemsSource = positions;
    }

    private void SetTitles()
    {
        Title = this.viewModel?.Id == 0 ?
                "Add new player" :
                $"Update {viewModel?.Name}";

        submitButton.Text = this.viewModel?.Id == 0 ?
                            "Save" :
                            $"Update";
    }

    private void SetActionPointer()
    {
        asyncAction = this.viewModel?.Id == 0 ?
                      AddNewPlayer :
                      UpdatePlayer;
    }

    private async Task AddNewPlayer()
    {
        var result = await playerClient.CreateAsync(viewModel);

        if (!result)
        {
            await DisplayAlert("ERROR", "There was an error while saving a new player!", "OK");
            return;
        }

        await DisplayAlert("", "New player saved successfully!", "OK");
    }

    private async Task UpdatePlayer()
    {
        var result = await playerClient.UpdateAsync(viewModel);

        if (!result)
        {
            await DisplayAlert("ERROR", "There was an error while updating a player!", "OK");
            return;
        }

        await DisplayAlert("", "Player updated successfully!", "OK");
    }

    private async void OnSubmitClick(object sender, EventArgs e)
    {
        if (this.viewModel?.HasErrors ?? true)
            return;

        await asyncAction();
        await Shell.Current.GoToAsync(PageRoutes.HomePage);
    }

    private void OnValidationHandler(Dictionary<string, string?> validationMessages)
    {
        if (validationMessages is null)
            return;

        lblValidationErrorName.Text = validationMessages.GetValueOrDefault("name");
        lblValidationErrorPosition.Text = validationMessages.GetValueOrDefault("positionid");
        lblValidationErrorClub.Text = validationMessages.GetValueOrDefault("club");
        lblValidationErrorWebImageLink.Text = validationMessages.GetValueOrDefault("webimagelink");
        lblValidationErrorBirthPlace.Text = validationMessages.GetValueOrDefault("birthplace");
        lblValidationErrorWeight.Text = validationMessages.GetValueOrDefault("weight");
        lblValidationErrorHeight.Text = validationMessages.GetValueOrDefault("height");
        lblValidationErrorDescription.Text = validationMessages.GetValueOrDefault("description");
    }
}