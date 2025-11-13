namespace KomaeiSample.Share.Dto;
public class LoginDto
{
    public required string Token { get; set; }
    public required string FullName { get; set; }
}

public class UserDto
{
    public Guid Id { get; set; }

    public string? FullName { get; set; }

    public string Mobile { get; set; } = null!;

    public string OrdersCount { get; set; } = null!;

    public bool IsAdmin { get; set; }

    public string LogoFileName { get; set; } = null!;

    public DateTime AddDate { get; set; }

    //public DateTime? UpdateDate { get; set; }
}