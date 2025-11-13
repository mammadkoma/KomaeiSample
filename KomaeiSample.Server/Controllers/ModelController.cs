namespace KomaeiSample.Server.Controllers;
public class ModelController(ModelService modelService) : AppController
{
    [HttpGet("{categoryId}")]
    public async Task<ActionResult> GetAllByCategoryId(int categoryId)
    {
        return Ok(await modelService.GetAllByCategoryId(categoryId));
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return Ok(await modelService.GetAll());
    }

    [HttpPost]
    [Authorize(Roles = $"{Constants.AdminRole},{Constants.SuperAdminRole}")]
    public async Task<ActionResult> Edit([FromForm] ModelVm vm, [FromForm] IFormFile? file)
    {
        return Ok(await modelService.Edit(vm, file));
    }
}