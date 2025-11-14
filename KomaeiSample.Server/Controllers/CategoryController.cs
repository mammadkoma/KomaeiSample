namespace KomaeiSample.Server.Controllers;

public class CategoryController(CategoryService categoryService) : AppController
{
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return Ok(await categoryService.GetAll());
    }

    [HttpPost]
    public async Task<ActionResult> Add(CategoryAddEditVm vm)
    {
        return Ok(await categoryService.Add(vm));
    }

    [HttpPost]
    public async Task<ActionResult> Edit(CategoryAddEditVm vm)
    {
        return Ok(await categoryService.Edit(vm));
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        return Ok(await categoryService.Delete(id));
    }
}