namespace KomaeiSample.Client.Components;
public partial class ChangeFullName
{
    ChangeFullNameVm vm = new();
    [CascadingParameter] public MainLayout? Layout { get; set; }
    string Mobile = "";

    protected override async Task OnInitializedAsync()
    {
        vm.FullName = (await localStorage.GetItem<string>("fullName"))!;
        Mobile = await http.GetStringAsync("User/GetCurrentUserMobile");
        await Task.CompletedTask;
    }

    private async Task Submit(EditContext context)
    {
        var httpResponseMessage = await http.PostAsJsonAsync("User/ChangeFullName", vm);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            snackbar.Add($"با موفقیت ذخیره شد", Severity.Success);
            await localStorage.SetItem("fullName", vm.FullName!);
            Layout!.userFullName = vm.FullName; Layout.Refresh();
        }
    }
}