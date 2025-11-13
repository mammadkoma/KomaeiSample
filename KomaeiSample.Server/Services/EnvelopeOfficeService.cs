namespace KomaeiSample.Server.Services;
public class EnvelopeOfficeService(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
{
    public async Task<EnvelopeOfficeDto[]> GetAll()
    {
        return await appDbContext.EnvelopeOffices.AsNoTracking().ProjectToType<EnvelopeOfficeDto>().ToArrayAsync();
    }

    public async Task<EnvelopeOfficeDto[]> GetAllEnables()
    {
        var result = await appDbContext.EnvelopeOffices.AsNoTracking().Where(x => x.Disable == false).ProjectToType<EnvelopeOfficeDto>().ToArrayAsync();
        return result.OrderBy(x => x.ModelId).ThenBy(x => x.PaperId).ThenBy(x => int.Parse(x.GrammageTitle)).ThenBy(x => int.Parse(x.CountTitle)).ThenBy(x => x.HasInternalTeram.ToHasOrNotTitle()).ThenBy(x => x.HasDoorGlue.ToHasOrNotTitle()).ToArray();
    }

    public async Task<int> Add(AddEditEnvelopeOfficeVm vm)
    {
        if (await appDbContext.EnvelopeOffices.AnyAsync(x => x.ModelId == vm.ModelId && x.PaperId == vm.PaperId && x.GrammageId == vm.GrammageId && x.CountId == vm.CountId && x.HasInternalTeram == vm.HasInternalTeram && x.HasDoorGlue == vm.HasDoorGlue))
            throw new AppException("این مورد قبلا ثبت شده است");
        var newRecord = vm.Adapt<EnvelopeOffice>();
        newRecord.AddUserId = httpContextAccessor.GetUserId();
        appDbContext.EnvelopeOffices.Add(newRecord);
        return await appDbContext.SaveChangesAsync();
    }

    public async Task<int> Edit(AddEditEnvelopeOfficeVm vm)
    {
        if (await appDbContext.EnvelopeOffices.AnyAsync(x => x.ModelId == vm.ModelId && x.PaperId == vm.PaperId && x.GrammageId == vm.GrammageId && x.CountId == vm.CountId && x.HasInternalTeram == vm.HasInternalTeram && x.HasDoorGlue == vm.HasDoorGlue && x.Id != vm.Id))
            throw new AppException("این مورد قبلا ثبت شده است");
        var record = await appDbContext.EnvelopeOffices.FirstOrDefaultAsync(x => x.Id == vm.Id);
        vm.Adapt(record);
        record!.UpdateUserId = httpContextAccessor.GetUserId();
        record!.UpdateDate = DateTime.Now;
        return await appDbContext.SaveChangesAsync();
    }
}