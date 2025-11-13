namespace KomaeiSample.Client.Layout;
public partial class PanelNavMenu
{
    [Inject] public AuthenticationStateProvider? authenticationStateProvider { get; set; }
    [Parameter] public string? UserFullName { get; set; }
    [Parameter] public string? CompanyName { get; set; }

    private async Task Logout()
    {
        var response = await http.PostAsync("User/Logout", null);
        if (response.IsSuccessStatusCode)
            await (authenticationStateProvider as AppAuthStateProvider)!.Logout(true);
    }
}