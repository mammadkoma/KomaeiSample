namespace KomaeiSample.Client.Pages.Panel;
public partial class Sliders
{
    SliderDto[] SliderDtos = Array.Empty<SliderDto>();

    protected override async Task OnInitializedAsync()
    {
        await GetData();
    }

    private async Task GetData()
    {
        SliderDtos = (await http.GetFromJsonAsync<SliderDto[]>("Slider/GetAll"))!;
    }

    private async Task DeleteRow(SliderDto row)
    {
        var parameters = new DialogParameters<ConfirmDialog>
        {
            { x => x.ContentText, "مطمئن هستید میخواهید این تصویر را حذف کنید؟" },
            { x => x.YesButtonText, "حذف" },
            { x => x.NoButtonText, "انصراف" },
            { x => x.Color, Color.Error }
        };
        var options = new DialogOptions { BackgroundClass = "backdrop-filter", CloseOnEscapeKey = true, CloseButton = true, FullWidth = true };
        var dialog = await dialogService.ShowAsync<ConfirmDialog>($"حذف تصویر اسلایدر ", parameters, options);
        var dialogResult = await dialog.Result;
        if (dialogResult!.Canceled == false && dialogResult.Data is true)
        {
            var response = await http.PostAsync("Slider/Delete/" + row.Id, null);
            if (response.IsSuccessStatusCode)
            {
                snackbar.Add($"با موفقیت حذف شد", Severity.Success);
                await GetData();
            }
        }
    }

    private async Task OpenAddDialog(SliderDto? row)
    {
        var parameters = new DialogParameters();
        if (row != null)
            parameters.Add("row", row);
        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };
        var dialog = await dialogService.ShowAsync<SliderAdd>(
            $"افزودن تصویر به اسلایدر", parameters, options);
        var dialogResult = await dialog.Result;
        if (dialogResult!.Canceled == false && dialogResult.Data is true)
            await GetData();
    }

    public async Task GoDown(int id)
    {
        var response = await http.PostAsync("Slider/GoDown/" + id, null);
        if (response.IsSuccessStatusCode)
            await GetData();
    }

    public async Task GoUp(int id)
    {
        var response = await http.PostAsync("Slider/GoUp/" + id, null);
        if (response.IsSuccessStatusCode)
            await GetData();
    }
}