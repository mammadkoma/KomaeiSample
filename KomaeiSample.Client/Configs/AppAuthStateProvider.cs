namespace KomaeiSample.Client.Configs;
public class AppAuthStateProvider : AuthenticationStateProvider
{
    private readonly LocalStorageService _localStorageService;
    private readonly NavigationManager _navigationManager;
    private readonly AuthenticationState _authenticationState;
    public AppAuthStateProvider(LocalStorageService localStorageService, NavigationManager navigationManager)
    {
        _localStorageService = localStorageService;
        _navigationManager = navigationManager;
        _authenticationState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var jwtToken = await _localStorageService.GetItem<string>("token");

        if (string.IsNullOrEmpty(jwtToken))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromToken(jwtToken), "JwtAuth"));
        return new AuthenticationState(claimsPrincipal);
    }


    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }

    public void NotifyLogin(string token)
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public static IEnumerable<Claim> ParseClaimsFromToken(string token)
    {
        var claims = new List<Claim>();
        var payload = token.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        ExtractRolesFromToken(claims, keyValuePairs!);
        claims.AddRange(keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!)));
        return claims;
    }

    private static void ExtractRolesFromToken(List<Claim> claims, Dictionary<string, object> keyValuePairs)
    {
        keyValuePairs.TryGetValue(Constants.RoleClaim, out object? role);
        if (role != null)
        {
            if (role.ToString()!.Length > 0)
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()!));
            keyValuePairs.Remove(Constants.RoleClaim);
        }
    }

    public void NotifyLogout()
    {
        var anonymousState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
        NotifyAuthenticationStateChanged(anonymousState);
    }

    public async Task Logout(bool forceLoad = false)
    {
        NotifyLogout();
        _navigationManager.NavigateTo("/Login", forceLoad: forceLoad);
        await _localStorageService.RemoveItem("token");
        await _localStorageService.RemoveItem("fullName");
    }
}