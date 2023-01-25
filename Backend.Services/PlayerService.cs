namespace Backend.Services;

public class PlayerService : IPlayerService
{
    private readonly ApplicationDbContext context;

    public PlayerService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<List<PlayerModel>> GetAllAsync()
    {
        return await context.Players.Include(x => x.Position)
                                    .Select(x => new PlayerModel(x))
                                    .AsNoTracking()
                                    .ToListAsync();
    }

    public async Task<List<PlayerModel>> PageAsync(int page = 0)
    {
        return await context.Players.Skip(page * 10)
                                    .Take(10)
                                    .Include(x => x.Position)
                                    .Select(x => new PlayerModel(x))
                                    .AsNoTracking()
                                    .ToListAsync();
    }

    public async Task<PlayerModel> GetByIdAsync(int id)
    {
        var entity = await context.Players.FindAsync(id);

        if (entity is null)
            throw new InvalidOperationException("Not found");
        
        return new PlayerModel(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await context.Players.FindAsync(id);
        
        if(entity is null)
            throw new InvalidOperationException("Not found.");

        context.Players.Remove(entity);
        context.SaveChanges();
    }

    public async Task CreateAsync(PlayerModel player)
    {
        if (player is null)
            throw new InvalidOperationException("Not found.");

        await context.Players.AddAsync(player.ToEntity());
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PlayerModel player)
    {
        if (player is null)
            throw new InvalidOperationException("Not found.");

        var entity = await context.Players.FindAsync(player.Id);

        if (entity is null)
            throw new InvalidOperationException("Not found.");

        player.ToEntity(entity);

        await context.SaveChangesAsync();
    }
}
