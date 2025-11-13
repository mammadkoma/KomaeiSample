namespace KomaeiSample.Share.Vm;
public class UserAddressAddEditVm
{
    public int? Id { get; set; }

    [Required(ErrorMessage = Constants.RequireMsg)]
    [MinLength(2, ErrorMessage = Constants.MinLengthMsg)]
    [MaxLength(50, ErrorMessage = Constants.MaxLengthMsg)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = Constants.RequireMsg)]
    [MinLength(10, ErrorMessage = Constants.MinLengthMsg)]
    [MaxLength(500, ErrorMessage = Constants.MaxLengthMsg)]
    public string Address { get; set; } = string.Empty;
}