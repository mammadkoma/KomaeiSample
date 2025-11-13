namespace KomaeiSample.Server.Config;
public static class AuthJwtTokenConfig
{
    public static void AddJwt(this IServiceCollection services)
    {
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.JwtSecretKey)),
                RoleClaimType = Constants.RoleClaim,
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
            };
            options.Events = new JwtBearerEvents
            {
                //OnAuthenticationFailed = context =>
                //{
                //    var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<TokenValidatorService>();
                //    return new Task(new Action(() => { }));
                //},
                OnTokenValidated = context =>
                {
                    var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<TokenValidatorService>();
                    return tokenValidatorService.ValidateAsync(context);
                },
                OnMessageReceived = context =>
                {
                    return Task.CompletedTask;
                },
            };
        });
    }
}