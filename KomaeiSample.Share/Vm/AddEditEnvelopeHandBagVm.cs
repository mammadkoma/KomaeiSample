namespace KomaeiSample.Share.Vm;
public class AddEditEnvelopeHandBagVm
{
    public int? Id { get; set; }

    [Required(ErrorMessage = Constants.RequireMsg)]
    public int? ModelId { get; set; }

    [Required(ErrorMessage = Constants.RequireMsg)]
    public int? PaperId { get; set; }

    [Required(ErrorMessage = Constants.RequireMsg)]
    public int? GrammageId { get; set; }

    [Required(ErrorMessage = Constants.RequireMsg)]
    public int? CountId { get; set; }

    [Required(ErrorMessage = Constants.RequireMsg)]
    public int? CellophaneId { get; set; }

    [Required(ErrorMessage = Constants.RequireMsg)]
    public int? UvId { get; set; }

    [Required(ErrorMessage = Constants.RequireMsg)]
    public decimal? Price { get; set; }

    public bool Disable { get; set; }
}
