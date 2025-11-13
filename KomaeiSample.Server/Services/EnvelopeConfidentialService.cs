namespace KomaeiSample.Server.Services;
public class EnvelopeConfidentialService(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
{
    public async Task<EnvelopeConfidentialDto[]> GetAll()
    {
        return await appDbContext.EnvelopeConfidentials.AsNoTracking().ProjectToType<EnvelopeConfidentialDto>().ToArrayAsync();
    }

    public async Task<EnvelopeConfidentialDto[]> GetAllEnables()
    {
        var result = await appDbContext.EnvelopeConfidentials.AsNoTracking().Where(x => x.Disable == false).ProjectToType<EnvelopeConfidentialDto>().ToArrayAsync();
        return result.OrderBy(x => x.ModelId).ThenBy(x => x.PaperTitle).ThenBy(x => int.Parse(x.GrammageTitle!)).ThenBy(x => int.Parse(x.CountTitle!)).ToArray();
    }

    public async Task<int> Add(AddEditEnvelopeConfidentialVm vm)
    {
        if (await appDbContext.EnvelopeConfidentials.AnyAsync(x => x.ModelId == vm.ModelId && x.PaperId == vm.PaperId && x.GrammageId == vm.GrammageId && x.CountId == vm.CountId))
            throw new AppException("این مورد قبلا ثبت شده است");
        var newRecord = vm.Adapt<EnvelopeConfidential>();
        newRecord.AddUserId = httpContextAccessor.GetUserId();
        appDbContext.EnvelopeConfidentials.Add(newRecord);
        return await appDbContext.SaveChangesAsync();
    }

    public async Task<int> Edit(AddEditEnvelopeConfidentialVm vm)
    {
        if (await appDbContext.EnvelopeConfidentials.AnyAsync(x => x.ModelId == vm.ModelId && x.PaperId == vm.PaperId && x.GrammageId == vm.GrammageId && x.CountId == vm.CountId && x.Id != vm.Id))
            throw new AppException("این مورد قبلا ثبت شده است");
        var record = await appDbContext.EnvelopeConfidentials.FirstOrDefaultAsync(x => x.Id == vm.Id);
        vm.Adapt(record);
        record!.UpdateUserId = httpContextAccessor.GetUserId();
        record!.UpdateDate = DateTime.Now;
        return await appDbContext.SaveChangesAsync();
    }
}