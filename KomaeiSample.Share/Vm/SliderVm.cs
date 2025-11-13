namespace KomaeiSample.Share.Vm;
public class SliderVm
{
    [Required(ErrorMessage = Constants.RequireMsg)]
    public IBrowserFile? File { get; set; }
}
