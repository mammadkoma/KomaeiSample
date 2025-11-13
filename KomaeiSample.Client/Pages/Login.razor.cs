namespace KomaeiSample.Client.Pages;
public partial class Login
{
    LoginVm vm = new();
    [Inject] public AuthenticationStateProvider authenticationStateProvider { get; set; } = default!;
    [CascadingParameter] public MainLayout? Layout { get; set; }
    private IJSObjectReference? _module;
    private InputType PasswordInputType = InputType.Password;
    private string PasswordIcon = Icons.Material.Filled.VisibilityOff;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        _module = await jSRuntime.InvokeAsync<IJSObjectReference>("import", "./Pages/Login.razor.js");
        await _module.InvokeVoidAsync("closeOffcanvas");
    }

    private async Task Submit(EditContext context)
    {
        var httpResponseMessage = await http.PostAsJsonAsync("User/Login", vm);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var loginDto = await httpResponseMessage.Content.ReadFromJsonAsync<LoginDto>();
            await localStorage.SetItem<string>("token", loginDto!.Token);
            await localStorage.SetItem<string>("fullName", loginDto.FullName);
            (authenticationStateProvider as AppAuthStateProvider)!.NotifyLogin(loginDto!.Token);
            Layout!.userFullName = loginDto.FullName;
            Layout.Refresh();
            navigationManager.NavigateTo("/");
        }
    }

    private void TogglePasswordVisibility()
    {
        if (PasswordInputType == InputType.Password)
        {
            PasswordInputType = InputType.Text;
            PasswordIcon = Icons.Material.Filled.Visibility;
        }
        else
        {
            PasswordInputType = InputType.Password;
            PasswordIcon = Icons.Material.Filled.VisibilityOff;
        }
    }
}