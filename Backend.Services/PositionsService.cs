namespace Backend.Services;

public class PositionService : IPositionService
{
    private readonly ApplicationDbContext context;

    public PositionService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<List<PositionModel>> GetAllAsync()
    {
        return await context.Positions.Select(x => new PositionModel(x))
                                      .AsNoTracking()
                                      .ToListAsync();
    }
}
