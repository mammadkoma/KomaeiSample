namespace KomaeiSample.Client.Pages.Panel;
public partial class TopSentence
{
    SettingDto vm = new();

    protected override async Task OnInitializedAsync()
    {
        vm = (await http.GetFromJsonAsync<SettingDto>("Setting/GetById/" + SettingEnum.TopSentence.ToInt()))!;
    }

    private async Task Submit(EditContext context)
    {
        var httpResponseMessage = await http.PostAsJsonAsync("Setting/Edit", vm);
        if (httpResponseMessage.IsSuccessStatusCode)
            snackbar.Add($"با موفقیت ذخیره شد", Severity.Success);
    }
}