namespace Backend.Database;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<PlayerEntity> Players { get; set; }
    public DbSet<PositionEntity> Positions { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        AddPositionsToDB(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    private void AddPositionsToDB(ModelBuilder modelBuilder)
    {
        var positions = new List<PositionEntity>
        {
            new PositionEntity { Id = 1, Name = "outside hitter"},
            new PositionEntity { Id = 2, Name = "opposite"},
            new PositionEntity { Id = 3, Name = "setter"},
            new PositionEntity { Id = 4, Name = "middle blocker"},
            new PositionEntity { Id = 5, Name = "libero"},
            new PositionEntity { Id = 6, Name = "defensive specialist"},
            new PositionEntity { Id = 7, Name = "serving specialist"}
        };

        modelBuilder.Entity<PositionEntity>().HasData(positions);
    }
}
