namespace Backend.Database.Entities;

[Table("Position")]
public class PositionEntity
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    public virtual IList<PlayerEntity> Players { get; set; }
}
