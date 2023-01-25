namespace Backend.Models;

public partial class RegisterModel : BaseViewModel
{
    private string userName;
    private string email;
    private string password;

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Username
    {
        get => this.userName;
        set
        {
            SetProperty(ref this.userName, value, true);

            ClearErrors();
            SetProperty(ref this.userName, value);
            ValidateAllProperties();
            OnPropertyChanged("ErrorDictionary[Username]");
        }
    }


    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email
    {
        get => this.email;
        set
        {
            SetProperty(ref this.email, value, true);

            ClearErrors();
            ValidateAllProperties();
            OnPropertyChanged("ErrorDictionary[Email]");
        }
    }

    [Required]
    [DataType(DataType.Password)]
    public string Password
    {
        get => this.password;
        set
        {
            SetProperty(ref this.password, value, true);

            ClearErrors();
            ValidateAllProperties();
            OnPropertyChanged("ErrorDictionary[Password]");
        }
    }
}
