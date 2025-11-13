namespace KomaeiSample.Share.Dto;
public class DiscountDto
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string CategoryTitle { get; set; } = null!;
    public string Code { get; set; } = null!;
    public byte Percent { get; set; }
    public decimal MaxPrice { get; set; }
    public bool Disable { get; set; }
    public required string AddUserFullName { get; set; }
    public required DateTime AddDate { get; set; }
    public string? UpdateUserFullName { get; set; }
    public DateTime? UpdateDate { get; set; }
}