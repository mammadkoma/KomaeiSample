namespace KomaeiSample.Client.Pages;
public partial class ForgetPassword
{
    MobileVm mobileVm = new();
    ChangePasswordInForgetVm changePasswordInForgetVm = new();
    byte step = 1;
    [CascadingParameter] public MainLayout? Layout { get; set; }
    private InputType PasswordInputType = InputType.Password;
    private string PasswordIcon = Icons.Material.Filled.VisibilityOff;
    private InputType PasswordInputType2 = InputType.Password;
    private string PasswordIcon2 = Icons.Material.Filled.VisibilityOff;

    private async Task MobileSubmit(EditContext context)
    {
        var httpResponseMessage = await http.PostAsJsonAsync("User/CreateNewConfirmCode", mobileVm);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            changePasswordInForgetVm.Id = await httpResponseMessage.Content.ReadFromJsonAsync<Guid>();
            step = 2;
        }
    }

    private async Task ChangePasswordSubmit(EditContext context)
    {
        var httpResponseMessage = await http.PostAsJsonAsync("User/ChangePasswordInForget", changePasswordInForgetVm);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            step = 3;
            StateHasChanged();
            snackbar.Add("تغییر پسورد با موفقیت انجام شد", Severity.Success);
            await Task.Delay(4000);
            navigationManager.NavigateTo("/Login");
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