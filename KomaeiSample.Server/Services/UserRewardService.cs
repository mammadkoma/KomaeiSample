namespace KomaeiSample.Server.Services;
public class UserRewardService(AppDbContext appDbContext, IHttpContextAccessor httpContext)
{
    public async Task<UserRewardProfileDto> GetUserRewardsForProfile()
    {
        var user = await appDbContext.Users.AsNoTracking().Where(x => x.Id == httpContext.GetUserId()).Select(x => new { x.Mobile, ReferrerUserMobile = x.ReferrerUser == null ? null : x.ReferrerUser.Mobile, ReferrerUserFullName = x.ReferrerUser == null ? null : x.ReferrerUser.FullName, x.WalletBalance }).FirstAsync();

        var result = new UserRewardProfileDto
        {
            ReferrerUserReferralCode = user.Mobile.ToReferralCode(),

            ReferredUserFullNameReferralCode = user.ReferrerUserMobile == null ? "" : user.ReferrerUserFullName + " (" + user.ReferrerUserMobile.ToReferralCode() + ")",

            WalletBalance = user.WalletBalance,

            UserRewardDtos = await appDbContext.UserRewards.AsNoTracking().Where(x => x.ReferrerUserId == httpContext.GetUserId()).OrderByDescending(x => x.Id).Select(x => new UserRewardDto
            {
                ReferredUserFullName = x.ReferredUser.FullName,
                ReferredUserMobile = x.ReferredUser.Mobile,
                UserRewardTypeId = x.UserRewardTypeId,
                Amount = x.Amount,
                Desc = x.Desc,
                AddDate = x.AddDate,
            }).ToArrayAsync(),
        };

        return result;
    }
}
