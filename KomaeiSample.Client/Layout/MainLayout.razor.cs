namespace KomaeiSample.Client.Layout;
public partial class MainLayout
{
    public string? userFullName { get; set; }

    protected override async Task OnInitializedAsync()
    {
        userFullName = await localStorage.GetItem<string>("fullName");
        apiService.ApiCountChanged += HandleApiCountChanged;
        await Task.CompletedTask;
    }

    private void HandleApiCountChanged()
    {
        StateHasChanged();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        snackbar.Configuration.VisibleStateDuration = 8000;
        snackbar.Configuration.HideTransitionDuration = 500;
        snackbar.Configuration.ShowTransitionDuration = 500;
        snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomEnd;
        snackbar.Configuration.ClearAfterNavigation = true;
        snackbar.Configuration.SnackbarVariant = Variant.Filled;

        if (firstRender)
        {
            var localStorageTheme = await localStorage.GetItem<string>("theme");
            StateHasChanged();
        }
    }

    public void Refresh()
    {
        StateHasChanged();
    }
}
