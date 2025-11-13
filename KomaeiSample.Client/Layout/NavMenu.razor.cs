namespace KomaeiSample.Client.Layout;
public partial class NavMenu
{
    private IJSObjectReference? _module;
    [Parameter] public required string UserFullName { get; set; }
    [Inject] public required AuthenticationStateProvider authenticationStateProvider { get; set; }
    [CascadingParameter] public required MainLayout Layout { get; set; }
    private string searchText = "";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _module = await jSRuntime.InvokeAsync<IJSObjectReference>("import", "./Layout/NavMenu.razor.js");
            await _module.InvokeVoidAsync("onLoad");
        }
    }

    private async Task Logout()
    {
        var response = await http.PostAsync("User/Logout", null);
        if (response.IsSuccessStatusCode)
            await (authenticationStateProvider as AppAuthStateProvider)!.Logout(true);
    }

    private void HandleSearchKeyDown(KeyboardEventArgs args)
    {
        if (args.Key == "Enter")
        {
            navigationManager.NavigateTo($"/Search/{searchText}");
        }
    }
}