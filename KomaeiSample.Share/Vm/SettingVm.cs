namespace KomaeiSample.Share.Vm;
public partial class SettingVm
{
    [Required(ErrorMessage = Constants.RequireMsg)]
    public int? Id { get; set; }

    [Required(ErrorMessage = Constants.RequireMsg)]
    public string? Value { get; set; }
}

public partial class SettingImageVm
{
    [Required(ErrorMessage = Constants.RequireMsg)]
    public int? Id { get; set; }

    public IBrowserFile? File { get; set; }
}