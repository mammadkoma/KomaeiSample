namespace KomaeiSample.Server.Controllers;
public class UserAddressController(UserAddressService userAddressService) : AppController
{
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return Ok(await userAddressService.GetAll());
    }

    [HttpPost]
    public async Task<ActionResult> Add(UserAddressAddEditVm vm)
    {
        return Ok(await userAddressService.Add(vm));
    }

    [HttpPost]
    public async Task<ActionResult> Edit(UserAddressAddEditVm vm)
    {
        return Ok(await userAddressService.Edit(vm));
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        return Ok(await userAddressService.Delete(id));
    }
}