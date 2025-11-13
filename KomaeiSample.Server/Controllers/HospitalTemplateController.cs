namespace KomaeiSample.Server.Controllers;
public class HospitalTemplateController(HospitalTemplateService hospitalTemplateService) : AppController
{
    [HttpGet("{modelId}")]
    public async Task<ActionResult> GetAllByModelId(int modelId)
    {
        return Ok(await hospitalTemplateService.GetAllByModelId(modelId));
    }

    [HttpPost("{id:int}")]
    [Authorize(Roles = $"{Constants.AdminRole},{Constants.SuperAdminRole}")]
    public async Task<ActionResult> Delete(int id)
    {
        return Ok(await hospitalTemplateService.Delete(id));
    }

    [HttpPost]
    [Authorize(Roles = $"{Constants.AdminRole},{Constants.SuperAdminRole}")]
    public async Task<ActionResult> Add([FromForm] HospitalTemplateVm vm, [FromForm] IFormFile? file)
    {
        return Ok(await hospitalTemplateService.Add(vm, file));
    }

    [HttpPost("{id:int}")]
    [Authorize(Roles = $"{Constants.AdminRole},{Constants.SuperAdminRole}")]
    public async Task<ActionResult> GoDown(int id)
    {
        return Ok(await hospitalTemplateService.GoDown(id));
    }

    [HttpPost("{id:int}")]
    [Authorize(Roles = $"{Constants.AdminRole},{Constants.SuperAdminRole}")]
    public async Task<ActionResult> GoUp(int id)
    {
        return Ok(await hospitalTemplateService.GoUp(id));
    }
}