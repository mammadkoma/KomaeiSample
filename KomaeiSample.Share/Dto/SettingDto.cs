namespace KomaeiSample.Share.Dto;
public partial class SettingDto
{
    public int Id { get; set; }

    public string Value { get; set; } = null!;
}

public partial class SettingDecimalDto
{
    public int Id { get; set; }

    public decimal Value { get; set; }
}