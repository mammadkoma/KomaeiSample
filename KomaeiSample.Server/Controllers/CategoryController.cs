namespace KomaeiSample.Server.Controllers;

public class CategoryController(CategoryService categoryService) : AppController
{
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return Ok(await categoryService.GetAll());
    }

    //[HttpPost]
    //[Authorize(Roles = $"{Constants.AdminRole},{Constants.SuperAdminRole}")]
    //public async Task<ActionResult> Edit([FromForm] CategoryVm vm, [FromForm] IFormFile? file)
    //{
    //    return Ok(await categoryService.Edit(vm, file));
    //}
}