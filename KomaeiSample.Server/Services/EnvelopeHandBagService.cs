namespace KomaeiSample.Server.Services;
public class EnvelopeHandBagService(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
{
    public async Task<EnvelopeHandBagDto[]> GetAll()
    {
        return await appDbContext.EnvelopeHandBags.AsNoTracking().ProjectToType<EnvelopeHandBagDto>().ToArrayAsync();
    }

    public async Task<EnvelopeHandBagDto[]> GetAllEnables()
    {
        var result = await appDbContext.EnvelopeHandBags.AsNoTracking().Where(x => x.Disable == false).ProjectToType<EnvelopeHandBagDto>().ToArrayAsync();
        return result.OrderBy(x => x.ModelId).ThenBy(x => x.PaperTitle).ThenBy(x => int.Parse(x.GrammageTitle!)).ThenBy(x => int.Parse(x.CountTitle!)).ThenBy(x => x.CellophaneTitle).ThenBy(x => x.UvTitle).ToArray();
    }

    public async Task<int> Add(AddEditEnvelopeHandBagVm vm)
    {
        if (await appDbContext.EnvelopeHandBags.AnyAsync(x => x.ModelId == vm.ModelId && x.PaperId == vm.PaperId && x.GrammageId == vm.GrammageId && x.CountId == vm.CountId && x.CellophaneId == vm.CellophaneId && x.UvId == vm.UvId))
            throw new AppException("این مورد قبلا ثبت شده است");
        var newRecord = vm.Adapt<EnvelopeHandBag>();
        newRecord.AddUserId = httpContextAccessor.GetUserId();
        appDbContext.EnvelopeHandBags.Add(newRecord);
        return await appDbContext.SaveChangesAsync();
    }

    public async Task<int> Edit(AddEditEnvelopeHandBagVm vm)
    {
        if (await appDbContext.EnvelopeHandBags.AnyAsync(x => x.ModelId == vm.ModelId && x.PaperId == vm.PaperId && x.GrammageId == vm.GrammageId && x.CountId == vm.CountId && x.CellophaneId == vm.CellophaneId && x.UvId == vm.UvId && x.Id != vm.Id))
            throw new AppException("این مورد قبلا ثبت شده است");
        var record = await appDbContext.EnvelopeHandBags.FirstOrDefaultAsync(x => x.Id == vm.Id);
        vm.Adapt(record);
        record!.UpdateUserId = httpContextAccessor.GetUserId();
        record!.UpdateDate = DateTime.Now;
        return await appDbContext.SaveChangesAsync();
    }
}