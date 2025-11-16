namespace KomaeiSample.Server.Controllers;

public class ProductController(ProductService productService) : AppController
{
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return Ok(await productService.GetAll());
    }

    [HttpPost]
    public async Task<ActionResult> AddEdit([FromForm] ProductVmServer vm)
    {
        return Ok(await productService.AddEdit(vm));
    }

    //[HttpPost("{id:int}")] // in card
    //public async Task<ActionResult> Delete(int id)
    //{
    //    return Ok(await orderService.Delete(id));
    //}
}