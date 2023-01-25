namespace MauiUI.ViewModels;

public class RegisterViewModel : RegisterModel
{
    private readonly ISecurityClient securityClient;

    public RegisterViewModel(ISecurityClient securityClient) : base()
	{
		this.securityClient = securityClient;
	}

    public ICommand NavigateToLoginPageCommand => new RelayCommand( async() =>
        await Shell.Current.GoToAsync(PageRoutes.LoginPage, true)
    );

    public ICommand RegisterCommand => new RelayCommand(OnRegisterCommand);

    private async void OnRegisterCommand()
    {
        if (this?.HasErrors ?? true)
            return;

        var requestParam = this.ConvertTo<RegisterModel>();

        var success = await securityClient.RegisterAsync(requestParam);

        if (!success)
        {
            await Application.Current.MainPage.DisplayAlert("", "Register faild", "OK");
            return;
        }
        await Application.Current.MainPage.DisplayAlert("", "Registered successfully.\nYou can now login.", "OK");

        await Shell.Current.GoToAsync(PageRoutes.LoginPage, true);
    }
}
