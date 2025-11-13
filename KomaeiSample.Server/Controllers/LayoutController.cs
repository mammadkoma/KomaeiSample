namespace KomaeiSample.Server.Controllers;
public class LayoutController(EnvelopeService envelopeService) : AppController
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult> GetAll()
    {
        return Ok(await envelopeService.GetAll());
    }
}