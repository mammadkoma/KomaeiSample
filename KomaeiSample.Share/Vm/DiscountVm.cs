namespace KomaeiSample.Share.Vm;
public class AddEditDiscountVm
{
    public int? Id { get; set; }

    [Required(ErrorMessage = Constants.RequireMsg)]
    public int? CategoryId { get; set; }

    [Required(ErrorMessage = Constants.RequireMsg)]
    [MaxLength(20, ErrorMessage = Constants.MaxLengthMsg)]
    public string Code { get; set; } = null!;

    [Required(ErrorMessage = Constants.RequireMsg)]
    [Range(1, 100, ErrorMessage = Constants.RangeMsg)]
    public byte Percent { get; set; }

    [Required(ErrorMessage = Constants.RequireMsg)]
    public decimal? MaxPrice { get; set; }

    public bool Disable { get; set; }
    public bool SendSms { get; set; }
}

public class GetDiscountByCodeAndCategoryIdVm
{
    [Required(ErrorMessage = Constants.RequireMsg)]
    [MaxLength(20, ErrorMessage = Constants.MaxLengthMsg)]
    public string Code { get; set; } = null!;

    [Required(ErrorMessage = Constants.RequireMsg)]
    public int? CategoryId { get; set; }
}