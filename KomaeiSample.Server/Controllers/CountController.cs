namespace KomaeiSample.Server.Controllers;
public class CountController(CountService modelService) : AppController
{
    [HttpGet("{categoryId}")]
    public async Task<ActionResult> GetAllByCategoryId(int categoryId)
    {
        return Ok(await modelService.GetAllByCategoryId(categoryId));
    }
}