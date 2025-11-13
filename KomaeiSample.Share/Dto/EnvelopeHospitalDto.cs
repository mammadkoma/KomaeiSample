namespace KomaeiSample.Share.Dto;
public class EnvelopeHospitalDto
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int ModelId { get; set; }
    public string? ModelTitle { get; set; }
    public string? ModelFileName { get; set; }
    public string? ModelFileExtension { get; set; }

    public int HospitalTemplateId { get; set; }
    public string? HospitalTemplateTemplateName { get; set; }
    public string? HospitalTemplateFileName { get; set; }
    public string? HospitalTemplateOrder { get; set; }

    public int PaperId { get; set; }
    public string? PaperTitle { get; set; }

    public int GrammageId { get; set; }
    public string? GrammageTitle { get; set; }

    public int CountId { get; set; }
    public string? CountTitle { get; set; }

    public int CellophaneId { get; set; }
    public string? CellophaneTitle { get; set; }

    public int UvId { get; set; }
    public string? UvTitle { get; set; }

    public decimal Price { get; set; }

    public bool Disable { get; set; }

    public Guid AddUserId { get; set; }
    public string? AddUserFullName { get; set; }

    public DateTime AddDate { get; set; }

    public Guid? UpdateUserId { get; set; }
    public string? UpdateUserFullName { get; set; }

    public DateTime? UpdateDate { get; set; }
}
