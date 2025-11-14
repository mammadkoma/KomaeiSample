namespace KomaeiSample.Server.Services;

public class CategoryService(AppDbContext appDbContext)
{
    public async Task<CategoryDto[]> GetAll()
    {
        return await appDbContext.Categories.AsNoTracking().ProjectToType<CategoryDto>().OrderBy(x => x.Title).ToArrayAsync();
    }

    public async Task<int> Add(CategoryAddEditVm vm)
    {
        var newRecord = vm.Adapt<Category>();
        appDbContext.Categories.Add(newRecord);
        return await appDbContext.SaveChangesAsync();
    }

    public async Task<int> Edit(CategoryAddEditVm vm)
    {
        return await appDbContext.Categories.Where(x => x.Id == vm.Id)
            .ExecuteUpdateAsync(x => x.SetProperty(x => x.Title, vm.Title));
    }

    public async Task<int> Delete(int id)
    {
        return await appDbContext.Categories.Where(u => u.Id == id).ExecuteDeleteAsync();
    }
}