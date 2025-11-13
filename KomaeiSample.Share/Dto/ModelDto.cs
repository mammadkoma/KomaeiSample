namespace KomaeiSample.Share.Dto;
public class ModelDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int CategoryId { get; set; }
    public string? FileName { get; set; }
    public string? FileExtension { get; set; }
    public string? Desc { get; set; }
    public string? MinPrice { get; set; }
    public string? MaxPrice { get; set; }
}