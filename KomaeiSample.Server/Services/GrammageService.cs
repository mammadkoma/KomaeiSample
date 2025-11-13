namespace KomaeiSample.Server.Services;
public class GrammageService(AppDbContext appDbContext)
{
    public async Task<GrammageDto[]> GetAllByCategoryId(int categoryId)
    {
        return await appDbContext.Grammages.AsNoTracking().Where(x => x.CategoryId == categoryId).ProjectToType<GrammageDto>().ToArrayAsync();
    }
}