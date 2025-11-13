namespace KomaeiSample.Share.Dto;
public partial class EnvelopeHandBagDto
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int ModelId { get; set; }
    public required string ModelTitle { get; set; }
    public string? ModelFileName { get; set; }
    public string? ModelFileExtension { get; set; }
    public int PaperId { get; set; }
    public required string PaperTitle { get; set; }
    public int GrammageId { get; set; }
    public required string GrammageTitle { get; set; }
    public int CountId { get; set; }
    public required string CountTitle { get; set; }
    public int CellophaneId { get; set; }
    public string? CellophaneTitle { get; set; }
    public int UvId { get; set; }
    public string? UvTitle { get; set; }
    public required decimal Price { get; set; }
    public required string AddUserFullName { get; set; }
    public required DateTime AddDate { get; set; }
    public string? UpdateUserFullName { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool Disable { get; set; }
}