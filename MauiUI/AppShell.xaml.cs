namespace MauiUI;
public partial class AppShell : Shell
{
    public AppShell()
    {
        RegisterRoutes();
        InitializeComponent();
    }

    private void RegisterRoutes()
    {
        Routing.RegisterRoute(PageRoutes.LoginPage, typeof(LoginPage));
        Routing.RegisterRoute(PageRoutes.RegisterPage, typeof(RegisterPage));
        Routing.RegisterRoute(PageRoutes.HomePage, typeof(MainPage));
        Routing.RegisterRoute(PageRoutes.DetailsPage, typeof(PlayerDetailsPage));
        Routing.RegisterRoute(PageRoutes.AddOrUpdatePage, typeof(AddOrUpdatePlayer));
    }
}