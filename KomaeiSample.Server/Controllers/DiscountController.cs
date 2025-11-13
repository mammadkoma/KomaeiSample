namespace KomaeiSample.Server.Controllers;
public class DiscountController(DiscountService discountService) : AppController
{
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return Ok(await discountService.GetAll());
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult> GetAllEnables()
    {
        return Ok(await discountService.GetAllEnables());
    }

    [HttpPost]
    public async Task<ActionResult> GetByCodeAndCategoryId(GetDiscountByCodeAndCategoryIdVm vm)
    {
        return Ok(await discountService.GetByCodeAndCategoryId(vm));
    }

    [HttpPost]
    [Authorize(Roles = $"{Constants.AdminRole},{Constants.SuperAdminRole}")]
    public async Task<ActionResult> Add(AddEditDiscountVm vm)
    {
        return Ok(await discountService.Add(vm));
    }

    [HttpPost]
    [Authorize(Roles = $"{Constants.AdminRole},{Constants.SuperAdminRole}")]
    public async Task<ActionResult> Edit(AddEditDiscountVm vm)
    {
        return Ok(await discountService.Edit(vm));
    }
}