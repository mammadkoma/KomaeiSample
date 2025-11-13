namespace KomaeiSample.Share.Dto;

public class ProductDto
{
    public int Id { get; set; }

    public string CategoryTitle { get; set; } = null!;

    public string Title { get; set; } = null!;

    public decimal Price { get; set; }
}
