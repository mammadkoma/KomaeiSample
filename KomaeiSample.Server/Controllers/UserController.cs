namespace KomaeiSample.Server.Controllers;
public class UserController(UserService userService, IHttpContextAccessor httpContext) : AppController
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> Register(RegisterVm vm)
    {
        return Ok(await userService.Register(vm));
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> ConfirmRegisterCode(ConfirmRegisterVm vm)
    {
        return Ok(await userService.ConfirmRegisterCode(vm));
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> Login(LoginVm vm)
    {
        return Ok(await userService.Login(vm));
    }

    [HttpPost]
    public async Task<ActionResult> Logout()
    {
        return Ok(await userService.Logout(httpContext.GetUserTokenId()));
    }

    [HttpPost]
    public async Task<ActionResult> ChangeFullName(ChangeFullNameVm vm)
    {
        return Ok(await userService.ChangeFullName(vm));
    }

    [HttpPost]
    public async Task<ActionResult> ChangePassword(ChangePasswordVm vm)
    {
        return Ok(await userService.ChangePassword(vm));
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> CreateNewConfirmCode(MobileVm vm)
    {
        return Ok(await userService.CreateNewConfirmCode(vm));
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> ChangePasswordInForget(ChangePasswordInForgetVm vm)
    {
        return Ok(await userService.ChangePasswordInForget(vm));
    }

    [HttpPost]
    public async Task<ActionResult> UpdateLogo([FromForm] IFormFile? file)
    {
        return Ok(await userService.UpdateLogo(file));
    }

    [HttpGet]
    public async Task<ActionResult> GetLogoFileName()
    {
        return Ok(await userService.GetLogoFileName());
    }

    [HttpGet]
    public async Task<ActionResult> GetAllConfirmed()
    {
        return Ok(await userService.GetAllConfirmed());
    }

    [HttpGet]
    public async Task<ActionResult> GetCurrentUserMobile()
    {
        return Ok(await userService.GetCurrentUserMobile());
    }
}