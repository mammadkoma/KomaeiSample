namespace KomaeiSample.Server.Services;

public class CategoryService(AppDbContext appDbContext)
{
    public async Task<CategoryDto[]> GetAll()
    {
        //var rsa = RSA.Create(2048);
        //var publicKey = rsa.ExportSubjectPublicKeyInfo();
        //var privateKey = rsa.ExportPkcs8PrivateKey();
        //var publicPem = PemEncoding.Write("PUBLIC KEY", publicKey);
        //var privatePem = PemEncoding.Write("PRIVATE KEY", privateKey);
        //File.WriteAllText("public.pem", publicPem);
        //File.WriteAllText("private.pem", privatePem);

        return await appDbContext.Categories.AsNoTracking().ProjectToType<CategoryDto>().OrderBy(x => x.Title).ToArrayAsync();
    }

    public async Task<int> Add(CategoryAddEditVm vm)
    {
        var newRecord = vm.Adapt<Category>();
        appDbContext.Categories.Add(newRecord);
        return await appDbContext.SaveChangesAsync();
    }

    public async Task<int> Edit(CategoryAddEditVm vm)
    {
        return await appDbContext.Categories.Where(x => x.Id == vm.Id)
            .ExecuteUpdateAsync(x => x.SetProperty(x => x.Title, vm.Title));
    }

    public async Task<int> Delete(int id)
    {
        return await appDbContext.Categories.Where(u => u.Id == id).ExecuteDeleteAsync();
    }
}