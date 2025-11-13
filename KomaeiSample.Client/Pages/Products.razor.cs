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

    private async Task OpenAddDialog(ProductDto? row)
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

    //private async Task OpenAddEditDialog(int orderId, int orderStatusId, string addUserMobile)
    //{
    //    var parameters = new DialogParameters();
    //    if (orderId > 0)
    //        parameters.Add("vm", new OrderEditVm { Id = orderId, OrderStatusId = orderStatusId, AddUserMobile = addUserMobile });
    //    var options = ConstantsClient.DialogOptionsSmall;
    //    var dialog = await dialogService.ShowAsync<OrderEdit>(
    //        "تغییر وضعیت سفارش", parameters, options);
    //    var dialogResult = await dialog.Result;
    //    if (dialogResult!.Canceled == false && dialogResult.Data is true)
    //    {
    //        snackbar.Add("وضعیت سفارش با موفقیت تغییر کرد", Severity.Success);
    //        await GetGridData();
    //    }
    //}
}