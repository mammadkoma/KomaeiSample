namespace KomaeiSample.Server.Services;
public class SettingService(AppDbContext appDbContext, FileService fileService)
{
    public async Task<SettingDto[]> GetAll()
    {
        return await appDbContext.Settings.AsNoTracking().ProjectToType<SettingDto>().ToArrayAsync();
    }

    public async Task<SettingDto> GetById(int id)
    {
        return await appDbContext.Settings.AsNoTracking().Where(x => x.Id == id).ProjectToType<SettingDto>().FirstAsync();
    }

    public async Task<SettingDto[]> GetAllByType(byte type)
    {
        return await appDbContext.Settings.AsNoTracking().Where(x => x.Type == type).ProjectToType<SettingDto>().ToArrayAsync();
    }

    public async Task<int> Edit(SettingVm vm)
    {
        return await appDbContext.Settings.Where(x => x.Id == vm.Id)
            .ExecuteUpdateAsync(x => x.SetProperty(x => x.Value, vm.Value));
    }

    public async Task<int> EditForImage(SettingImageVm vm, IFormFile? file)
    {
        if (file == null)
            return 1;
        var record = await appDbContext.Settings.Where(x => x.Id == vm.Id).FirstAsync();
        fileService.Delete(record.Value.Split(".")[0], "." + record.Value.Split(".")[1], "Image");
        var newGuid = Guid.NewGuid().ToString();
        record.Value = newGuid + Path.GetExtension(file.FileName);
        await fileService.WriteAsync(newGuid, file, "Image");
        return await appDbContext.SaveChangesAsync();
    }
}