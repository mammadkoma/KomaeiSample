namespace KomaeiSample.Share.Dto;
public class HospitalTemplateDto
{
    public int Id { get; set; }

    public int ModelId { get; set; }

    public string TemplateName { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public short Order { get; set; }
}