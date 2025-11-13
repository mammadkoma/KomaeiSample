namespace KomaeiSample.Server.Services;
public class EnvelopeHospitalService(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
{
    public async Task<EnvelopeHospitalDto[]> GetAll()
    {
        return await appDbContext.EnvelopeHospitals.AsNoTracking().ProjectToType<EnvelopeHospitalDto>().ToArrayAsync();
    }

    public async Task<EnvelopeHospitalDto[]> GetAllEnables()
    {
        var result = await appDbContext.EnvelopeHospitals.AsNoTracking().Where(x => x.Disable == false).ProjectToType<EnvelopeHospitalDto>().ToArrayAsync();
        return result.OrderBy(x => x.ModelId).ThenBy(x => x.HospitalTemplateTemplateName).ThenBy(x => x.PaperTitle).ThenBy(x => int.Parse(x.GrammageTitle!)).ThenBy(x => int.Parse(x.CountTitle!)).ThenBy(x => x.CellophaneTitle).ThenBy(x => x.UvTitle).ToArray();
    }

    public async Task<int> Add(AddEditEnvelopeHospitalVm vm)
    {
        if (await appDbContext.EnvelopeHospitals.AnyAsync(x => x.ModelId == vm.ModelId && x.HospitalTemplateId == vm.HospitalTemplateId && x.PaperId == vm.PaperId && x.GrammageId == vm.GrammageId && x.CountId == vm.CountId && x.CellophaneId == vm.CellophaneId && x.UvId == vm.UvId))
            throw new AppException("این مورد قبلا ثبت شده است");
        if (vm.HospitalTemplateId == null)
            throw new AppException("قالب را انتخاب کنید");
        var newRecord = vm.Adapt<EnvelopeHospital>();
        newRecord.AddUserId = httpContextAccessor.GetUserId();
        appDbContext.EnvelopeHospitals.Add(newRecord);
        return await appDbContext.SaveChangesAsync();
    }

    public async Task<int> Edit(AddEditEnvelopeHospitalVm vm)
    {
        if (await appDbContext.EnvelopeHospitals.AnyAsync(x => x.ModelId == vm.ModelId && x.HospitalTemplateId == vm.HospitalTemplateId && x.PaperId == vm.PaperId && x.GrammageId == vm.GrammageId && x.CountId == vm.CountId && x.CellophaneId == vm.CellophaneId && x.UvId == vm.UvId && x.Id != vm.Id))
            throw new AppException("این مورد قبلا ثبت شده است");
        if (vm.HospitalTemplateId == null)
            throw new AppException("قالب را انتخاب کنید");
        var record = await appDbContext.EnvelopeHospitals.FirstOrDefaultAsync(x => x.Id == vm.Id);
        vm.Adapt(record);
        record!.UpdateUserId = httpContextAccessor.GetUserId();
        record!.UpdateDate = DateTime.Now;
        return await appDbContext.SaveChangesAsync();
    }
}