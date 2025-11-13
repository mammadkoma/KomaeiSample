namespace KomaeiSample.Server.Config;
public class TokenService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public TokenService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId => Guid.Parse(_httpContextAccessor.HttpContext!.User.Claims
        .FirstOrDefault(c => c.Type == Constants.UserIdClaim)?.Value!);

    public string GenerateToken(User user, int userTokenId)
    {
        var role = Constants.UserRole;
        if (user.IsAdmin == true)
            role = Constants.AdminRole;
        if (user.Mobile == "09999999999")
            role = Constants.SuperAdminRole;
        var claims = new[]
        {
            new Claim(Constants.UserTokenIdClaim, userTokenId.ToString()),
            new Claim(Constants.UserIdClaim, user.Id.ToString()),
            new Claim(Constants.RoleClaim, role),
            //new Claim(Constants.MobileClaim, user.Mobile),
        };

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.JwtSecretKey));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokenOptions = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
}