namespace KomaeiSample.Share.Dto;
public class UserRewardDto
{
    public string? ReferredUserFullName { get; set; }
    public string? ReferredUserMobile { get; set; }
    public int UserRewardTypeId { get; set; }
    public decimal Amount { get; set; }
    public string? Desc { get; set; }
    public DateTime AddDate { get; set; }
}

public class UserRewardProfileDto
{
    public string ReferrerUserReferralCode { get; set; } = null!;
    public string? ReferredUserFullNameReferralCode { get; set; } = null!;
    public decimal WalletBalance { get; set; }
    public UserRewardDto[]? UserRewardDtos { get; set; } = Array.Empty<UserRewardDto>();
}