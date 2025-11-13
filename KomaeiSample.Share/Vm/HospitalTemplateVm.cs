namespace KomaeiSample.Share.Vm;
public class HospitalTemplateVm
{
    public int ModelId { get; set; }

    public string? TemplateName { get; set; }

    public IBrowserFile? File { get; set; }
}
