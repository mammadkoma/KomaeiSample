namespace KomaeiSample.Server.Controllers;
public class SettingController(SettingService settingService) : AppController
{
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult> GetById(int id)
    {
        return Ok(await settingService.GetById(id));
    }

    [HttpGet("{type}")]
    public async Task<ActionResult> GetAllByType(byte type)
    {
        return Ok(await settingService.GetAllByType(type));
    }

    [HttpPost]
    [Authorize(Roles = $"{Constants.AdminRole},{Constants.SuperAdminRole}")]
    public async Task<ActionResult> Edit(SettingVm vm)
    {
        return Ok(await settingService.Edit(vm));
    }

    [HttpPost]
    [Authorize(Roles = $"{Constants.AdminRole},{Constants.SuperAdminRole}")]
    public async Task<ActionResult> EditForImage([FromForm] SettingImageVm vm, [FromForm] IFormFile? file)
    {
        return Ok(await settingService.EditForImage(vm, file));
    }
}