namespace Backend.Models;

public partial class LoginModel : BaseViewModel
{
    private string email = string.Empty;
    private string password = string.Empty;


    [Required]
    [MinLength(5)]
    //TODO: add email validation annotation
    public string Email
    {
        get => this.email;
        set
        {
            SetProperty(ref this.email, value, true);
            
            OnPropertyChanged("ErrorDictionary[Email]");
        }
    }

    [Required]
    [DataType(DataType.Password)]
    [MinLength(8)]
    public string Password
    {
        get => this.password;
        set
        {
            SetProperty(ref this.password, value, true);
            OnPropertyChanged("ErrorDictionary[Password]");
        }
    }
}
