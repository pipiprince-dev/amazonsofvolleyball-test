namespace Backend.Database.Entities;

[Table("Player")]
public class PlayerEntity
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    [Required]
    [StringLength(4069)]
    public string WebImageLink { get; set; }

    [Required]
    [StringLength(255)]
    public string Club { get; set; }

    [Required]
    [StringLength(32)]
    public string Birthday { get; set; }

    [Required]
    [StringLength(255)]
    public string BirthPlace { get; set; }

    [Required]
    [Range(0, 100)]
    public int Weight { get; set; }

    [Required]
    [Range(0, 2.5)]
    public double Height { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    [ForeignKey("Position")]
    public int PositionId { get; set; }

    [Required]
    [StringLength(255)]
    public virtual PositionEntity Position { get; set; }
}
