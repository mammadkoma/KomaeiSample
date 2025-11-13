namespace KomaeiSample.Server.Services;
public class UserTokenService(AppDbContext appDbContext)
{
    public async Task<int> Add(Guid userId, string token)
    {
        var userToken = new UserToken
        {
            UserId = userId,
            Token = token,
            ExpireDate = DateTime.Now.AddDays(Constants.JwtExpireDays)
        };
        appDbContext.Add(userToken);
        await appDbContext.SaveChangesAsync();
        return userToken.Id;
    }

    public async Task<int> UpdateToken(int id, string token)
    {
        return await appDbContext.UserTokens.Where(ut => ut.Id == id)
        .ExecuteUpdateAsync(ut => ut.SetProperty(ut => ut.Token, token));
    }

    public async Task<bool> IsValidTokenAsync(int id, string token)
    {
        var userToken = await appDbContext.UserTokens.FirstOrDefaultAsync(ut => ut.Id == id);

        if (userToken == null || userToken.Token != token)
            return false;

        var now = DateTime.Now;
        var daysDifference = now.GetDaysDifference(userToken.ExpireDate);

        if (daysDifference < 0)
        {
            appDbContext.UserTokens.Remove(userToken);
            await appDbContext.SaveChangesAsync();
            return false;
        }

        if (daysDifference >= 0 && daysDifference < Constants.JwtRefreshDays)
        {
            userToken.ExpireDate = now.AddDays(Constants.JwtExpireDays);
            await appDbContext.SaveChangesAsync();
        }

        return true;
    }

    public async Task<int> Delete(int id)
    {
        return await appDbContext.UserTokens.Where(ut => ut.Id == id).ExecuteDeleteAsync();
    }
}