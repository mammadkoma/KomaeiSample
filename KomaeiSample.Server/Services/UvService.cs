namespace KomaeiSample.Server.Services;
public class UvService(AppDbContext appDbContext)
{
    public async Task<UvDto[]> GetAll()
    {
        return await appDbContext.Uvs.AsNoTracking().ProjectToType<UvDto>().ToArrayAsync();
    }
}