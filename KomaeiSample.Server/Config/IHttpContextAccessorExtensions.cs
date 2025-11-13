namespace KomaeiSample.Server.Config;
public static class IHttpContextAccessorExtensions
{
    public static Guid GetUserId(this IHttpContextAccessor httpContextAccessor)
    {
        return Guid.Parse(httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(c => c.Type == Constants.UserIdClaim)?.Value!);
    }

    //public static string GetUserMobile(this IHttpContextAccessor httpContextAccessor)
    //{
    //    return httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(c => c.Type == Constants.MobileClaim)?.Value!;
    //}

    public static int GetUserTokenId(this IHttpContextAccessor httpContextAccessor)
    {
        return int.Parse(httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(c => c.Type == Constants.UserTokenIdClaim)?.Value!);
    }

    //public static string GetMobile(this IHttpContextAccessor httpContextAccessor)
    //{
    //    return httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(c => c.Type == Constants.MobileClaim)?.Value!;
    //}
}