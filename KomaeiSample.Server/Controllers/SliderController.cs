namespace KomaeiSample.Server.Controllers;
public class SliderController(SliderService sliderService) : AppController
{
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return Ok(await sliderService.GetAll());
    }

    [HttpPost("{id:int}")]
    [Authorize(Roles = $"{Constants.AdminRole},{Constants.SuperAdminRole}")]
    public async Task<ActionResult> Delete(int id)
    {
        return Ok(await sliderService.Delete(id));
    }

    [HttpPost]
    [Authorize(Roles = $"{Constants.AdminRole},{Constants.SuperAdminRole}")]
    public async Task<ActionResult> Add([FromForm] IFormFile? file)
    {
        return Ok(await sliderService.Add(file));
    }

    [HttpPost("{id:int}")]
    [Authorize(Roles = $"{Constants.AdminRole},{Constants.SuperAdminRole}")]
    public async Task<ActionResult> GoDown(int id)
    {
        return Ok(await sliderService.GoDown(id));
    }

    [HttpPost("{id:int}")]
    [Authorize(Roles = $"{Constants.AdminRole},{Constants.SuperAdminRole}")]
    public async Task<ActionResult> GoUp(int id)
    {
        return Ok(await sliderService.GoUp(id));
    }
}