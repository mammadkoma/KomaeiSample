namespace KomaeiSample.Server.Services;
public class ModelService(AppDbContext appDbContext, FileService fileService)
{
    public async Task<ModelDto[]> GetAllByCategoryId(int categoryId)
    {
        return await appDbContext.Models.AsNoTracking().Where(x => x.CategoryId == categoryId).ProjectToType<ModelDto>().ToArrayAsync();
    }

    public async Task<ModelDto[]> GetAll()
    {
        return await appDbContext.Models.AsNoTracking().ProjectToType<ModelDto>().ToArrayAsync();
    }

    public async Task<int> Edit(ModelVm vm, IFormFile? file)
    {
        if (file == null)
            return 1;
        var record = await appDbContext.Models.Where(x => x.Id == vm.Id).FirstAsync();
        fileService.Delete(record.FileName!, record.FileExtension!, "Model");
        record.FileName = Guid.NewGuid().ToString();
        record.FileExtension = Path.GetExtension(file.FileName);
        await fileService.WriteAsync(record.FileName, file, "Model");
        return await appDbContext.SaveChangesAsync();
    }
}