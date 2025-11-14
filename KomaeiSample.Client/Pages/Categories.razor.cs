using KomaeiSample.Client.Components;

namespace KomaeiSample.Client.Pages;

public partial class Categories
{
    CategoryDto[] CategoryDtos = Array.Empty<CategoryDto>();

    protected override async Task OnInitializedAsync()
        => await GetGridData();

    private async Task GetGridData() =>
        CategoryDtos = (await http.GetFromJsonAsync<CategoryDto[]>("Category/GetAll"))!;

    private async Task OpenAddEditDialog(CategoryDto? row)
    {
        var parameters = new DialogParameters();
        if (row != null) parameters.Add("row", row);
        var options = ConstantsClient.DialogOptionsSmall;
        var dialog = await dialogService.ShowAsync<CategoryAddEdit>("افزودن دسته", parameters, options);
        var dialogResult = await dialog.Result;
        if (dialogResult!.Canceled == false && dialogResult.Data is true)
            await GetGridData();
    }

    private async Task Delete(CategoryDto row)
    {
        var parameters = new DialogParameters<ConfirmDialog>
        {
            { x => x.ContentText, "مطمئن هستید؟" },
            { x => x.YesButtonText, "حذف" },
            { x => x.NoButtonText, "انصراف" },
            { x => x.Color, Color.Error }
        };
        var options = new DialogOptions { BackgroundClass = "backdrop-filter", CloseOnEscapeKey = true, CloseButton = true, FullWidth = true };
        var dialog = await dialogService.ShowAsync<ConfirmDialog>($"حذف دسته : {row.Title}", parameters, options);
        var dialogResult = await dialog.Result;
        if (dialogResult!.Canceled == false && dialogResult.Data is true)
        {
            var httpResponseMessage = await http.DeleteAsync($"Category/Delete?id={row.Id}");
            if (httpResponseMessage.IsSuccessStatusCode)
                await GetGridData();
        }
    }
}