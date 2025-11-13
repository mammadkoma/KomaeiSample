namespace KomaeiSample.Server.Services;

public class CategoryService(AppDbContext appDbContext, FileService fileService)
{
    public async Task<CategoryDto[]> GetAll()
    {
        return await appDbContext.Categories.AsNoTracking().ProjectToType<CategoryDto>().ToArrayAsync();
    }

    //public async Task<int> Edit(CategoryVm vm, IFormFile? file)
    //{
    //    if (file == null)
    //        return 1;
    //    var record = await appDbContext.Categories.Where(x => x.Id == vm.Id).FirstAsync();
    //    fileService.Delete(record.FileName!, record.FileExtension!, "Category");
    //    record.FileName = Guid.NewGuid().ToString();
    //    record.FileExtension = Path.GetExtension(file.FileName);
    //    await fileService.WriteAsync(record.FileName, file, "Category");
    //    return await appDbContext.SaveChangesAsync();
    //}
}