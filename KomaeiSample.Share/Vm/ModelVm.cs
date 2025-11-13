namespace KomaeiSample.Share.Vm;
public partial class ModelVm
{
    public int? CategoryId { get; set; }

    [Required(ErrorMessage = Constants.RequireMsg)]
    public int? Id { get; set; }

    public IBrowserFile? File { get; set; }
}