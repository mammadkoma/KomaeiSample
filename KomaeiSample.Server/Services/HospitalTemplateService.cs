namespace KomaeiSample.Server.Services;
public class HospitalTemplateService(AppDbContext appDbContext, FileService fileService)
{
    public async Task<HospitalTemplateDto[]> GetAllByModelId(int modelId)
    {
        return await appDbContext.HospitalTemplates.Where(x => x.ModelId == modelId).AsNoTracking().ProjectToType<HospitalTemplateDto>().OrderBy(x => x.Order).ToArrayAsync();
    }

    public async Task<HospitalTemplateDto[]> GetAll()
    {
        return await appDbContext.HospitalTemplates.AsNoTracking().ProjectToType<HospitalTemplateDto>().OrderBy(x => x.Order).ToArrayAsync();
    }

    public async Task<int> Delete(int id)
    {
        if (await appDbContext.HospitalTemplates.CountAsync() == 1)
            throw new AppException("باید حداقل یک قالب وجود داشته باشد");
        var record = await appDbContext.HospitalTemplates.Where(x => x.Id == id).FirstAsync();
        fileService.Delete(record.FileName.Split(".")[0], "." + record.FileName.Split(".")[1], "HospitalTemplate");
        appDbContext.HospitalTemplates.Remove(record);
        return await appDbContext.SaveChangesAsync();
    }

    public async Task<int> Add([FromForm] HospitalTemplateVm vm, [FromForm] IFormFile? file)
    {
        if (file == null)
            return 1;
        var fileName = Guid.NewGuid().ToString();
        await fileService.WriteAsync(fileName, file, "HospitalTemplate");
        var maxOrder = await appDbContext.HospitalTemplates.MaxAsync(x => (int?)x.Order) ?? 0;
        var newRecord = new HospitalTemplate { ModelId = vm.ModelId, TemplateName = vm.TemplateName!, FileName = fileName + Path.GetExtension(file.FileName), Order = (short)(maxOrder + 1) };
        appDbContext.HospitalTemplates.Add(newRecord);
        return await appDbContext.SaveChangesAsync();
    }

    public async Task<int> GoDown(int id)
    {
        var record = await appDbContext.HospitalTemplates.FindAsync(id);
        var belowRecord = await appDbContext.HospitalTemplates.FirstAsync(x => x.Order == record!.Order + 1);
        record!.Order = (short)(record.Order + 1);
        belowRecord!.Order = (short)(belowRecord.Order - 1);
        return await appDbContext.SaveChangesAsync();
    }

    public async Task<int> GoUp(int id)
    {
        var record = await appDbContext.HospitalTemplates.FindAsync(id);
        var upperRecord = await appDbContext.HospitalTemplates.FirstAsync(i => i.Order == record!.Order - 1);
        record!.Order = (short)(record.Order - 1);
        upperRecord!.Order = (short)(upperRecord.Order + 1);
        return await appDbContext.SaveChangesAsync();
    }
}