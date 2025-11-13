namespace KomaeiSample.Share.Dto;
public class LayoutDto
{
    public CategoryDto[]? AllCategories { get; set; } = Array.Empty<CategoryDto>();
    public SettingDto[]? SettingDtos { get; set; } = Array.Empty<SettingDto>();
    public SliderDto[]? SliderDtos { get; set; } = Array.Empty<SliderDto>();
    public List<CardDto>? CardDtos { get; set; } = new List<CardDto>();
}

public class CardDto
{
    public int CategoryId { get; set; }
    public string? CategoryTitle { get; set; }
    public int ModelId { get; set; }
    public required string ModelTitle { get; set; }
    public string? ModelFileName { get; set; }
    public string? ModelFileExtension { get; set; }
    public string? PapersTitle { get; set; }
    public string? GrammagesTitle { get; set; }
    public string? CountsTitle { get; set; }
    public required decimal MinPrice { get; set; }
    public required decimal MaxPrice { get; set; }
}

public class CardRawDto
{
    public int CategoryId { get; set; }
    public required string CategoryTitle { get; set; }
    public int ModelId { get; set; }
    public required string ModelTitle { get; set; }
    public required string ModelFileName { get; set; }
    public required string ModelFileExtension { get; set; }
    public int PaperId { get; set; }
    public required string PaperTitle { get; set; }
    public int GrammageId { get; set; }
    public required string GrammageTitle { get; set; }
    public int CountId { get; set; }
    public required string CountTitle { get; set; }
    public decimal Price { get; set; }
}