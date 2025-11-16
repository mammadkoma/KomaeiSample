namespace KomaeiSample.Server.Config;
public static class IHttpContextAccessorExtensions
{
    public static Guid GetUserId(this IHttpContextAccessor httpContextAccessor)
    {
        return Guid.Parse(httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(c => c.Type == Constants.UserIdClaim)?.Value!);
    }

    public static int GetUserTokenId(this IHttpContextAccessor httpContextAccessor)
    {
        return int.Parse(httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(c => c.Type == Constants.UserTokenIdClaim)?.Value!);
    }
}