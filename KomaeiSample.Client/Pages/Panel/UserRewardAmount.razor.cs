namespace KomaeiSample.Client.Pages.Panel;
public partial class UserRewardAmount
{
    SettingDto vmTemp = new();
    SettingDecimalDto vm = new();

    protected override async Task OnInitializedAsync()
    {
        vmTemp = (await http.GetFromJsonAsync<SettingDto>("Setting/GetById/" + SettingEnum.UserRewardAmount.ToInt()))!;
        vm = vmTemp.Adapt<SettingDecimalDto>();
    }

    private async Task OnValidSubmit(EditContext context)
    {
        var httpResponseMessage = await http.PostAsJsonAsync("Setting/Edit", vm.Adapt<SettingDto>());
        if (httpResponseMessage.IsSuccessStatusCode)
            snackbar.Add($"با موفقیت ذخیره شد", Severity.Success);
    }
}