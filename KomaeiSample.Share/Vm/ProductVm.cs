using Microsoft.AspNetCore.Http;

namespace KomaeiSample.Share.Vm;

public class ProductVm
{
    public int? Id { get; set; }

    [Required(ErrorMessage = Constants.RequireMsg)]
    public int? CategoryId { get; set; }

    [Required(ErrorMessage = Constants.RequireMsg)]
    [MinLength(2, ErrorMessage = Constants.MinLengthMsg)]
    [MaxLength(100, ErrorMessage = Constants.MaxLengthMsg)]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = Constants.RequireMsg)]
    public decimal? Price { get; set; }
}

public class ProductVmClient : ProductVm
{
    [Required(ErrorMessage = Constants.RequireMsg)]
    public IBrowserFile? File { get; set; }
    public IFormFile? FileForServer { get; set; }
}

public class ProductVmServer : ProductVm
{
    public IBrowserFile? FileForClient { get; set; }

    [Required(ErrorMessage = Constants.RequireMsg)]
    public IFormFile? FileForServer { get; set; }
}