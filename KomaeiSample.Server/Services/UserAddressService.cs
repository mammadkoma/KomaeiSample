namespace KomaeiSample.Server.Services;
public class UserAddressService(AppDbContext appDbContext, IHttpContextAccessor httpContext)
{
    public async Task<UserAddressDto[]> GetAll()
    {
        return await appDbContext.UserAddresses.AsNoTracking().ProjectToType<UserAddressDto>().ToArrayAsync();
    }

    public async Task<int> Add(UserAddressAddEditVm vm)
    {
        var newRecord = vm.Adapt<UserAddress>();
        newRecord.UserId = httpContext.GetUserId();
        appDbContext.UserAddresses.Add(newRecord);
        return await appDbContext.SaveChangesAsync();
    }

    public async Task<int> Edit(UserAddressAddEditVm vm)
    {
        return await appDbContext.UserAddresses.Where(x => x.Id == vm.Id)
            .ExecuteUpdateAsync(x => x
                .SetProperty(x => x.Title, vm.Title)
                .SetProperty(x => x.Address, vm.Address)
            );
    }

    public async Task<int> Delete(int id)
    {
        return await appDbContext.UserAddresses.Where(u => u.Id == id).ExecuteDeleteAsync();
    }
}