namespace KomaeiSample.Share.Dto;
public class ShareDto
{
}

public class ModelPaperGrammageCountDto
{
    public ModelDto[]? Models { get; set; }
    public PaperDto[]? Papers { get; set; }
    public GrammageDto[]? Grammages { get; set; }
    public CountDto[]? Counts { get; set; }
}

public class EnvelopeHospitalAddEditPageDto
{
    public ModelDto[]? Models { get; set; }
    public HospitalTemplateDto[]? HospitalTemplates { get; set; }
    public PaperDto[]? Papers { get; set; }
    public GrammageDto[]? Grammages { get; set; }
    public CountDto[]? Counts { get; set; }
    public CellophaneDto[]? Cellophanes { get; set; }
    public UvDto[]? Uvs { get; set; }
}

public class ModelPaperGrammageCountCellophaneUvDto
{
    public ModelDto[]? Models { get; set; }
    public PaperDto[]? Papers { get; set; }
    public GrammageDto[]? Grammages { get; set; }
    public CountDto[]? Counts { get; set; }
    public CellophaneDto[]? Cellophanes { get; set; }
    public UvDto[]? Uvs { get; set; }
}
