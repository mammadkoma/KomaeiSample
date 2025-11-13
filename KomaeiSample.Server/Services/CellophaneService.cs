namespace KomaeiSample.Server.Services;
public class CellophaneService(AppDbContext appDbContext)
{
    public async Task<CellophaneDto[]> GetAll()
    {
        return await appDbContext.Cellophanes.AsNoTracking().ProjectToType<CellophaneDto>().ToArrayAsync();
    }
}