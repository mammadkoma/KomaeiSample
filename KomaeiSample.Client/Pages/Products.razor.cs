namespace KomaeiSample.Client.Pages;

public partial class Products
{
    ProductDto[] ProductDtos = Array.Empty<ProductDto>();

    protected override async Task OnInitializedAsync()
    {
        await GetGridData();
    }

    private async Task GetGridData()
    {
        ProductDtos = (await http.GetFromJsonAsync<ProductDto[]>("Product/GetAll"))!;
    }

    private async Task OpenAddEditDialog(ProductDto? row)
    {
        var parameters = new DialogParameters();
        if (row != null) parameters.Add("row", row);
        var options = ConstantsClient.DialogOptionsMedium;
        var dialog = await dialogService.ShowAsync<ProductAddEdit>(
            $"افزودن محصول", parameters, options);
        var dialogResult = await dialog.Result;
        if (dialogResult!.Canceled == false && dialogResult.Data is true)
            await GetGridData();

    }
}