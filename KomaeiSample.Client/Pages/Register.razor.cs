namespace KomaeiSample.Client.Pages;
public partial class Register
{
    RegisterVm registerVm = new();
    ConfirmRegisterVm confirmRegisterVm = new();
    byte step = 1;
    [CascadingParameter] public MainLayout? Layout { get; set; }
    InputType PasswordInputType = InputType.Password;
    string PasswordIcon = Icons.Material.Filled.VisibilityOff;
    InputType PasswordInputType2 = InputType.Password;
    string PasswordIcon2 = Icons.Material.Filled.VisibilityOff;

    private async Task RegisterSubmit(EditContext context)
    {
        var httpResponseMessage = await http.PostAsJsonAsync("User/Register", registerVm);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            confirmRegisterVm.Id = await httpResponseMessage.Content.ReadFromJsonAsync<Guid>();
            step = 2;
        }
    }

    private async Task ConfirmCodeSubmit(EditContext context)
    {
        var httpResponseMessage = await http.PostAsJsonAsync("User/ConfirmRegisterCode", confirmRegisterVm);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            step = 3;
            StateHasChanged();
            snackbar.Add("ثبت نام با موفقیت انجام شد", Severity.Success);
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