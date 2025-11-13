namespace KomaeiSample.Client.Pages;
public partial class Home
{
    [CascadingParameter] public required MainLayout Layout { get; set; }
    bool isDesktop = true;

    protected override async Task OnInitializedAsync()
    {
        isDesktop = await jSRuntime.InvokeAsync<bool>("isDesktop");
    }
}
