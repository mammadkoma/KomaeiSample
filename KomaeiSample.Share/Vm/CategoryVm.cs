namespace KomaeiSample.Share.Vm;

public partial class CategoryAddEditVm
{
    public int? Id { get; set; }

    [Required(ErrorMessage = Constants.RequireMsg)]
    [MinLength(2, ErrorMessage = Constants.MinLengthMsg)]
    [MaxLength(50, ErrorMessage = Constants.MaxLengthMsg)]
    public string Title { get; set; } = string.Empty;
}