namespace KomaeiSample.Client.Layout;
public partial class PanelLayout
{
    bool drawerOpen = true;
    private bool isDarkMode;
    [CascadingParameter]
    public string? userFullName { get; set; }
    public string? today;
    public string? todayTooltip;
    ErrorBoundary? errorBoundary = default!;

    protected override void OnParametersSet()
    {
        errorBoundary!.Recover();
    }

    protected override async Task OnInitializedAsync()
    {
        userFullName = await localStorage.GetItem<string>("fullName");
        today = DateTime.Now.ToShamsiDate();
        todayTooltip = $"{DateTime.Now.ToShamsiDayName()} {DateTime.Now.ToShamsiDay()} {DateTime.Now.ToShamsiMonthName()}";
        apiService.ApiCountChanged += HandleApiCountChanged;
    }

    private void HandleApiCountChanged()
    {
        StateHasChanged();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        snackbar.Configuration.VisibleStateDuration = 10000;
        snackbar.Configuration.HideTransitionDuration = 500;
        snackbar.Configuration.ShowTransitionDuration = 500;
        snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomEnd;
        snackbar.Configuration.ClearAfterNavigation = true;
        snackbar.Configuration.SnackbarVariant = Variant.Filled;

        if (firstRender)
        {
            var localStorageTheme = await localStorage.GetItem<string>("theme");
            if (localStorageTheme != null)
                isDarkMode = localStorageTheme == "dark";
            StateHasChanged();
        }
    }

    private async Task ToggleDarkMode()
    {
        isDarkMode = !isDarkMode;
        await localStorage.SetItem<string>("theme", isDarkMode ? "dark" : "light");
    }

    public void Refresh()
    {
        StateHasChanged();
    }
}
