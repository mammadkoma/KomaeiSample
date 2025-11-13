

using KomaeiSample.Server.Services;

namespace KomaeiSample.Server.Config;
public class TokenValidatorService
{
    private readonly UserTokenService _userTokenService;
    public TokenValidatorService(UserTokenService userTokenService)
    {
        _userTokenService = userTokenService;
    }

    public async Task ValidateAsync(TokenValidatedContext context)
    {
        var claimsIdentity = context.Principal!.Identity as ClaimsIdentity;
        if (claimsIdentity?.Claims == null || claimsIdentity.Claims.Any() == false)
        {
            context.Fail("token is not ours");
            return;
        }

        var userTokenIdClaim = context.Principal.Claims.First(c => c.Type == Constants.UserTokenIdClaim);

        if (context.Principal.Claims == null || userTokenIdClaim == null || string.IsNullOrWhiteSpace(context.SecurityToken.ToString()))
        {
            context.Fail("token is not valid");
            return;
        }

        var refreshToken = int.Parse(userTokenIdClaim!.Value);
        var token = context.Request.Headers["Authorization"].ToString().Split(" ")[1];
        if (!await _userTokenService.IsValidTokenAsync(refreshToken, token))
        {
            context.Fail("token is expired");
            return;
        }
    }
}