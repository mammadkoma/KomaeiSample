namespace KomaeiSample.Client.Components;
public partial class ChangePassword
{
    ChangePasswordVm vm = new();
    [Inject] public required AuthenticationStateProvider authenticationStateProvider { get; set; }
    private InputType PasswordInputType0 = InputType.Password;
    private string PasswordIcon0 = Icons.Material.Filled.VisibilityOff;
    private InputType PasswordInputType1 = InputType.Password;
    private string PasswordIcon1 = Icons.Material.Filled.VisibilityOff;
    private InputType PasswordInputType2 = InputType.Password;
    private string PasswordIcon2 = Icons.Material.Filled.VisibilityOff;
    [CascadingParameter] public MainLayout? Layout { get; set; }
    string Mobile = "";

    protected override async Task OnInitializedAsync()
    {
        Mobile = await http.GetStringAsync("User/GetCurrentUserMobile");
    }

    private async Task Submit(EditContext context)
    {
        var httpResponseMessage = await http.PostAsJsonAsync("User/ChangePassword", vm);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            await (authenticationStateProvider as AppAuthStateProvider)!.Logout();
            snackbar.Add($"پسورد جدید با موفقیت ثبت شد", Severity.Success);
        }
    }

    private void TogglePasswordVisibility0()
    {
        if (PasswordInputType0 == InputType.Password)
        {
            PasswordInputType0 = InputType.Text;
            PasswordIcon0 = Icons.Material.Filled.Visibility;
        }
        else
        {
            PasswordInputType0 = InputType.Password;
            PasswordIcon0 = Icons.Material.Filled.VisibilityOff;
        }
    }

    private void TogglePasswordVisibility1()
    {
        if (PasswordInputType1 == InputType.Password)
        {
            PasswordInputType1 = InputType.Text;
            PasswordIcon1 = Icons.Material.Filled.Visibility;
        }
        else
        {
            PasswordInputType1 = InputType.Password;
            PasswordIcon1 = Icons.Material.Filled.VisibilityOff;
        }
    }

    private void TogglePasswordVisibility2()
    {
        if (PasswordInputType2 == InputType.Password)
        {
            PasswordInputType2 = InputType.Text;
            PasswordIcon2 = Icons.Material.Filled.Visibility;
        }
        else
        {
            PasswordInputType2 = InputType.Password;
            PasswordIcon2 = Icons.Material.Filled.VisibilityOff;
        }
    }
}