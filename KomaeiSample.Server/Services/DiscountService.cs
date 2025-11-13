namespace KomaeiSample.Server.Services;
public class DiscountService(AppDbContext appDbContext, IHttpContextAccessor httpContext, SmsService smsService)
{
    public async Task<DiscountDto[]> GetAll()
    {
        return await appDbContext.Discounts.AsNoTracking().ProjectToType<DiscountDto>().OrderByDescending(x => x.Id).ToArrayAsync();
    }

    public async Task<DiscountDto[]> GetAllEnables()
    {
        return await appDbContext.Discounts.AsNoTracking().Where(x => x.Disable == false).ProjectToType<DiscountDto>().OrderByDescending(x => x.Id).Take(200).ToArrayAsync();
    }

    public async Task<DiscountDto> GetByCodeAndCategoryId(GetDiscountByCodeAndCategoryIdVm vm)
    {
        var result = (await appDbContext.Discounts.AsNoTracking().Where(x => x.Code == vm.Code && x.CategoryId == vm.CategoryId && x.Disable == false).ProjectToType<DiscountDto>().FirstOrDefaultAsync())!;

        if (result == null)
            throw new AppException("این کد تخفیف معتبر نیست");

        else if (await appDbContext.UserDiscounts.AnyAsync(x => x.UserId == httpContext.GetUserId() && x.DiscountId == result.Id))
            throw new AppException("این کد تخفیف را استفاده کرده اید");

        else
            return result;
    }

    public async Task<int> Add(AddEditDiscountVm vm)
    {
        if (await appDbContext.Discounts.AnyAsync(x => x.CategoryId == vm.CategoryId && x.Code == vm.Code))
            throw new AppException("این مورد قبلا ثبت شده است");
        var newRecord = vm.Adapt<Discount>();
        newRecord.AddUserId = httpContext.GetUserId();
        appDbContext.Discounts.Add(newRecord);
        await appDbContext.SaveChangesAsync();
        if (vm.SendSms)
        {
            var excludeMobiles = new List<string>(["09111111111", "09888888888", "09999999999"]);
            var mobiles = await appDbContext.Users.Where(x => excludeMobiles.Contains(x.Mobile) == false).Select(x => x.Mobile).ToListAsync();
            foreach (var mobile in mobiles)
            {
                _ = smsService.Send(mobile, "\"خبر خوب\"\r\nتخفیف جدید r\n*ویژه همکاران*\r\nتا پایان فصل پاییز\r\nhttps://KomaeiSample.ir/discounts");
            }
        }
        return 1;
    }

    public async Task<int> Edit(AddEditDiscountVm vm)
    {
        if (await appDbContext.Discounts.AnyAsync(x => x.CategoryId == vm.CategoryId && x.Code == vm.Code && x.Id != vm.Id))
            throw new AppException("این مورد قبلا ثبت شده است");
        var record = await appDbContext.Discounts.FirstOrDefaultAsync(x => x.Id == vm.Id);
        vm.Adapt(record);
        record!.UpdateUserId = httpContext.GetUserId();
        record!.UpdateDate = DateTime.Now;
        return await appDbContext.SaveChangesAsync();
    }
}