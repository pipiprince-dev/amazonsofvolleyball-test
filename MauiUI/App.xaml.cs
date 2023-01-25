namespace MauiUI;
public partial class App : Application
{
    private readonly LoginViewModel loginViewModel;

    public App(LoginViewModel loginViewModel)
    {
        this.loginViewModel = loginViewModel;
        InitializeComponent();

        MainPage = new SplashPage();

        MainThread.InvokeOnMainThreadAsync( async () =>
        {
            await Task.Delay(2000);
            await SetStartPage();
        });
    }

    private async Task SetStartPage()
    {
        var hasValidJWT = await ReatJwtAsync();

        MainPage = hasValidJWT ?
                   new AppShell() :
                   new LoginPage(loginViewModel);
    }

    private async Task<bool> ReatJwtAsync()
    {
        var jwt = await StorageService.Secure.GetAsync<JWTokenModel>(StorageKeys.Secure.JWT);

        return !(jwt is null || jwt?.Expiration < DateTime.Now);
    }
}