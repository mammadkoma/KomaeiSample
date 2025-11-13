namespace KomaeiSample.Server.Controllers;
public class PaperController(PaperService paperService) : AppController
{
    [HttpGet("{categoryId}")]
    public async Task<ActionResult> GetAllByCategoryId(int categoryId)
    {
        return Ok(await paperService.GetAllByCategoryId(categoryId));
    }
}