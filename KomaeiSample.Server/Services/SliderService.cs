namespace KomaeiSample.Server.Services;
public class SliderService(AppDbContext appDbContext, FileService fileService)
{
    public async Task<SliderDto[]> GetAll()
    {
        return await appDbContext.Sliders.AsNoTracking().ProjectToType<SliderDto>().OrderBy(x => x.Order).ToArrayAsync();
    }

    public async Task<int> Delete(int id)
    {
        if (await appDbContext.Sliders.CountAsync() == 1)
            throw new AppException("باید حداقل یک تصویر وجود داشته باشد");
        var record = await appDbContext.Sliders.Where(x => x.Id == id).FirstAsync();
        fileService.Delete(record.FileName.Split(".")[0], "." + record.FileName.Split(".")[1], "Slider");
        appDbContext.Sliders.Remove(record);
        return await appDbContext.SaveChangesAsync();
    }

    public async Task<int> Add(IFormFile? file)
    {
        if (file == null)
            return 1;
        var fileName = Guid.NewGuid().ToString();
        await fileService.WriteAsync(fileName, file, "Slider");
        var maxOrder = await appDbContext.Sliders.MaxAsync(x => (int?)x.Order) ?? 0;
        var newRecord = new Slider { FileName = fileName + Path.GetExtension(file.FileName), Order = (short)(maxOrder + 1) };
        appDbContext.Sliders.Add(newRecord);
        return await appDbContext.SaveChangesAsync();
    }

    public async Task<int> GoDown(int id)
    {
        var record = await appDbContext.Sliders.FindAsync(id);
        var belowRecord = await appDbContext.Sliders.FirstAsync(x => x.Order == record!.Order + 1);
        record!.Order = (short)(record.Order + 1);
        belowRecord!.Order = (short)(belowRecord.Order - 1);
        return await appDbContext.SaveChangesAsync();
    }

    public async Task<int> GoUp(int id)
    {
        var record = await appDbContext.Sliders.FindAsync(id);
        var upperRecord = await appDbContext.Sliders.FirstAsync(i => i.Order == record!.Order - 1);
        record!.Order = (short)(record.Order - 1);
        upperRecord!.Order = (short)(upperRecord.Order + 1);
        return await appDbContext.SaveChangesAsync();
    }
}