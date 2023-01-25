namespace MauiUI.Pages;

public partial class LoginPage : ContentPage
{
    private RegisterViewModel viewModel => BindingContext as RegisterViewModel;

    public LoginPage(LoginViewModel viewModel)
	{
        InitializeComponent();

        viewModel.ValidationCompleted += OnValidationHandler;
        BindingContext = viewModel;

#if ANDROID
        MauiUI.Platforms.Android.KeyboardHelper.HideKeyboard();
#elif IOS
        MauiUI.Platforms.iOS.KeyboardHelper.HideKeyboard();
#endif
    }

    private void OnValidationHandler(Dictionary<string, string> validationMessages)
    {
        if (validationMessages is null)
            return;

        lblValidationErrorEmail.Text = validationMessages.GetValueOrDefault("email");
        lblValidationErrorPassword.Text = validationMessages.GetValueOrDefault("password");
    }
}