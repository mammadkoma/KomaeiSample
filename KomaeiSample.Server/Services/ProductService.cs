namespace KomaeiSample.Server.Services;

public class ProductService(AppDbContext appDbContext, FileService fileService)
{
    public async Task<ProductDto[]> GetAll()
    {
        return await appDbContext.Products.AsNoTracking().ProjectToType<ProductDto>().ToArrayAsync();
    }

    public async Task<int> AddEdit(ProductVmServer vm)
    {
        if (vm.Id == null)
        {
            var product = new Product
            {
                CategoryId = vm.CategoryId!.Value,
                Title = vm.Title,
                Price = vm.Price!.Value,
                FileName = Guid.NewGuid().ToString(),
                FileExtension = Path.GetExtension(vm.FileForServer!.FileName)
            };
            await fileService.WriteAsync(product.FileName, vm.FileForServer, "Product");
            appDbContext.Products.Add(product);
        }
        else
        {
            var product = await appDbContext.Products.FirstOrDefaultAsync(p => p.Id == vm.Id);
            product!.CategoryId = vm.CategoryId!.Value;
            product.Title = vm.Title;
            product.Price = vm.Price!.Value;
        }

        return await appDbContext.SaveChangesAsync();
    }
}