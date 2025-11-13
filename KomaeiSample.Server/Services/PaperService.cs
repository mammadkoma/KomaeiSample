namespace KomaeiSample.Server.Services;
public class PaperService(AppDbContext appDbContext)
{
    public async Task<PaperDto[]> GetAllByCategoryId(int categoryId)
    {
        return await appDbContext.Papers.AsNoTracking().Where(x => x.CategoryId == categoryId).ProjectToType<PaperDto>().ToArrayAsync();
    }
}