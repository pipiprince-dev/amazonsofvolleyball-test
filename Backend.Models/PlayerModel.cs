namespace Backend.Models;

public partial class PlayerModel : BaseViewModel
{
    private int id;
    private string name;
    private string webImageLink;
    private string club;
    private string birthday;
    private string birthPlace;
    private int? weight;
    private double? height;
    private string description;
    private PositionModel position;
    
    public int Id
    {
        get => this.id;
        set => SetProperty(ref this.id, value, true);
    }

    [Required]
    [StringLength(255)]
    [MinLength(2)]
    public string Name
    {
        get => this.name;
        set
        {
            SetProperty(ref this.name, value, true);

            ClearErrors();
            ValidateAllProperties();
            OnPropertyChanged("ErrorDictionary[Name]");
        }
    }

    [Required]
    [StringLength(4096)]
    public string WebImageLink
    {
        get => this.webImageLink;
        set
        {
            SetProperty(ref this.webImageLink, value, true);

            ClearErrors();
            ValidateAllProperties();
            OnPropertyChanged("ErrorDictionary[WebImageLink]");
        }
    }

    [Required]
    [StringLength(255)]
    [MinLength(2)]
    public string Club
    {
        get => this.club;
        set
        {
            SetProperty(ref this.club, value, true);

            ClearErrors();
            ValidateAllProperties();
            OnPropertyChanged("ErrorDictionary[Club]");
        }
    }

    [Required]
    [StringLength(32)]
    public string Birthday
    {
        get => this.birthday;
        set
        {
            SetProperty(ref this.birthday, value, true);

            ClearErrors();
            ValidateAllProperties();
            OnPropertyChanged("ErrorDictionary[Birthday]");
        }
    }

    [Required]
    [StringLength(255)]
    public string BirthPlace
    {
        get => this.birthPlace;
        set
        {
            SetProperty(ref this.birthPlace, value, true);

            ClearErrors();
            ValidateAllProperties();
            OnPropertyChanged("ErrorDictionary[BirthPlace]");
        }
    }

    [Required]
    [Range(0, 100)]
    public int? Weight
    {
        get => this.weight;
        set
        {
            SetProperty(ref this.weight, value, true);

            ClearErrors();
            ValidateAllProperties();
            OnPropertyChanged("ErrorDictionary[Weight]");
        }
    }

    [Required]
    [Range(0, 2.5)]
    public double? Height
    {
        get => this.height;
        set
        {
            SetProperty(ref this.height, value, true);

            ClearErrors();
            ValidateAllProperties();
            OnPropertyChanged("ErrorDictionary[Height]");
        }
    }

    [Required]
    public string Description
    {
        get => this.description;
        set
        {
            SetProperty(ref this.description, value, true);

            ClearErrors();
            ValidateAllProperties();
            OnPropertyChanged("ErrorDictionary[Description]");
        }
    }

    [Required]
    public PositionModel Position
    {
        get => this.position;
        set
        {
            SetProperty(ref this.position, value, true);

            ClearErrors();
            ValidateAllProperties();
            OnPropertyChanged("ErrorDictionary[Position]");
        }
    }

    public PlayerModel() : base()
    {
    }

    public PlayerModel(int id, string name, string webImageLink, string club, string birthday, string birthPlace, int weight, double height, string description, string positionName, int positionId) : base()
    {
        Id = id;
        Name = name;
        WebImageLink = webImageLink;
        Club = club;
        Birthday = birthday;
        BirthPlace = birthPlace;
        Weight = weight;
        Height = height;
        Description = description;
        Position = new PositionModel(positionId, positionName);
    }

    public PlayerModel(int id, string name, string webImageLink, string club, string birthday, string birthPlace, int weight, double height, string description, PositionModel position) : base()
    {
        Id = id;
        Name = name;
        WebImageLink = webImageLink;
        Club = club;
        Birthday = birthday;
        BirthPlace = birthPlace;
        Weight = weight;
        Height = height;
        Description = description;
        Position = position;
    }

    public PlayerModel(PlayerEntity player)
    {
        Id = player.Id;
        Name = player.Name;
        WebImageLink = player.WebImageLink;
        Club = player.Club;
        Birthday = player.Birthday;
        BirthPlace = player.BirthPlace;
        Weight = player.Weight;
        Height = player.Height;
        Description = player.Description;
        Position = new PositionModel(player.Position);
    }

    public PlayerEntity ToEntity()
    {
        return new PlayerEntity
        {
            Id = Id,
            Name = Name,
            WebImageLink = WebImageLink,
            Club = Club,
            Birthday = Birthday,
            BirthPlace = BirthPlace,
            Weight = Weight.Value,
            Height = Height.Value,
            Description = Description,
            PositionId = Position.Id
        };
    }

    public void ToEntity(PlayerEntity player)
    {
        player.Id = Id;
        player.Name = Name;
        player.WebImageLink = WebImageLink;
        player.Club = Club;
        player.Birthday = Birthday;
        player.BirthPlace = BirthPlace;
        player.Weight = Weight.Value;
        player.Height = Height.Value;
        player.Description = Description;

        player.PositionId = Position.Id;
    }
}