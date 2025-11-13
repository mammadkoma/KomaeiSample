namespace KomaeiSample.Server.Services;
public class CountService(AppDbContext appDbContext)
{
    public async Task<CountDto[]> GetAllByCategoryId(int categoryId)
    {
        return await appDbContext.Counts.AsNoTracking().Where(x => x.CategoryId == categoryId).ProjectToType<CountDto>().ToArrayAsync();
    }
}