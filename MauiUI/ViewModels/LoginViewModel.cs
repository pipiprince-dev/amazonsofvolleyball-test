namespace MauiUI.ViewModels;

public partial class LoginViewModel : LoginModel
{
    private readonly ISecurityClient securityClient;

    public LoginViewModel(ISecurityClient securityClient) : base()
    {
        this.securityClient = securityClient;
    }

    public ICommand LoginCommand => new RelayCommand(async() => await LoginAsync());

    public ICommand NavigateToRegisterPageCommand => new RelayCommand(async () => await Shell.Current.GoToAsync(PageRoutes.RegisterPage, true));

    private async Task LoginAsync()
    {
        
        //ValidateModel();
  
        if (this?.HasErrors ?? true)
            return;

        var requestParam = new LoginModel
        { 
            Email = this.Email,
            Password = this.Password
        };

        var response = await securityClient.LoginAsync(requestParam);

        if (response is null)
        {
            await Application.Current.MainPage.DisplayAlert("", "Login faild, or unauthorized", "OK");
            StorageService.Secure.Remove(StorageKeys.Secure.JWT);
            return;
        }

        await StorageService.Secure.SaveAsync<JWTokenModel>(StorageKeys.Secure.JWT, response);

        await Shell.Current.GoToAsync(PageRoutes.HomePage, true);
    }
}
