namespace KomaeiSample.Share.Vm;
public class EnvelopeVm
{
}

public class UpdateMultiplePricesVm
{
    public CategoriesEnum CategoriesEnum { get; set; }

    [Required(ErrorMessage = Constants.RequireMsg)]
    public UpdateMultiplePriceType UpdateMultiplePriceType { get; set; } = UpdateMultiplePriceType.Price;

    public decimal? Price { get; set; }

    public decimal? Percent { get; set; }
}