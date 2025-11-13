namespace KomaeiSample.Server.Controllers;
public class EnvelopeController(AppDbContext appDbContext, IHttpContextAccessor httpContext) : AppController
{
    [HttpPost]
    [Authorize(Roles = $"{Constants.AdminRole},{Constants.SuperAdminRole}")]
    public async Task<ActionResult> UpdateMultiplePrices(UpdateMultiplePricesVm vm)
    {
        if (vm.CategoriesEnum == CategoriesEnum.Office)
        {
            var records = await appDbContext.EnvelopeOffices.ToListAsync();
            foreach (var record in records)
            {
                if (vm.UpdateMultiplePriceType == UpdateMultiplePriceType.Price)
                    record.Price += vm.Price!.Value;
                else if (vm.UpdateMultiplePriceType == UpdateMultiplePriceType.Percent)
                    record.Price += (record.Price * vm.Percent!.Value / 100);
                record.UpdateDate = DateTime.Now;
                record.UpdateUserId = httpContext.GetUserId();
            }
            await appDbContext.SaveChangesAsync();
        }
        else if (vm.CategoriesEnum == CategoriesEnum.Hospital)
        {
            var records = await appDbContext.EnvelopeHospitals.ToListAsync();
            foreach (var record in records)
            {
                if (vm.UpdateMultiplePriceType == UpdateMultiplePriceType.Price)
                    record.Price += vm.Price!.Value;
                else if (vm.UpdateMultiplePriceType == UpdateMultiplePriceType.Percent)
                    record.Price += (record.Price * vm.Percent!.Value / 100);
                record.UpdateDate = DateTime.Now;
                record.UpdateUserId = httpContext.GetUserId();
            }
            await appDbContext.SaveChangesAsync();
        }
        else if (vm.CategoriesEnum == CategoriesEnum.HandBag)
        {
            var records = await appDbContext.EnvelopeHandBags.ToListAsync();
            foreach (var record in records)
            {
                if (vm.UpdateMultiplePriceType == UpdateMultiplePriceType.Price)
                    record.Price += vm.Price!.Value;
                else if (vm.UpdateMultiplePriceType == UpdateMultiplePriceType.Percent)
                    record.Price += (record.Price * vm.Percent!.Value / 100);
                record.UpdateDate = DateTime.Now;
                record.UpdateUserId = httpContext.GetUserId();
            }
            await appDbContext.SaveChangesAsync();
        }
        else if (vm.CategoriesEnum == CategoriesEnum.Confidential)
        {
            var records = await appDbContext.EnvelopeConfidentials.ToListAsync();
            foreach (var record in records)
            {
                if (vm.UpdateMultiplePriceType == UpdateMultiplePriceType.Price)
                    record.Price += vm.Price!.Value;
                else if (vm.UpdateMultiplePriceType == UpdateMultiplePriceType.Percent)
                    record.Price += (record.Price * vm.Percent!.Value / 100);
                record.UpdateDate = DateTime.Now;
                record.UpdateUserId = httpContext.GetUserId();
            }
            await appDbContext.SaveChangesAsync();
        }
        return Ok();
    }
}