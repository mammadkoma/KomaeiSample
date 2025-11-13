namespace KomaeiSample.Share.Vm;
public partial class CategoryVm
{
    [Required(ErrorMessage = Constants.RequireMsg)]
    public int? Id { get; set; }

    public IBrowserFile? File { get; set; }
}