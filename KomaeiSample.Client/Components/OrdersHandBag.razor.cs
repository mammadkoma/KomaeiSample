namespace KomaeiSample.Client.Components;
public partial class OrdersHandBag
{
    [Parameter] public OrderHandBagDto[]? OrderHandBagDtos { get; set; }
    [Parameter] public bool HasAdminColumns { get; set; } = false;

    private async Task GetGridData()
    {
        OrderHandBagDtos = (await http.GetFromJsonAsync<OrderDto>("Order/GetAll"))!.OrderHandBagDtos;
    }

    private async Task OpenAddEditDialog(int orderId, int orderStatusId, string addUserMobile)
    {
        var parameters = new DialogParameters();
        if (orderId > 0)
            parameters.Add("vm", new OrderEditVm { Id = orderId, OrderStatusId = orderStatusId, AddUserMobile = addUserMobile });
        var options = ConstantsClient.DialogOptionsSmall;
        var dialog = await dialogService.ShowAsync<OrderEdit>(
            "تغییر وضعیت سفارش", parameters, options);
        var dialogResult = await dialog.Result;
        if (dialogResult!.Canceled == false && dialogResult.Data is true)
        {
            snackbar.Add("وضعیت سفارش با موفقیت تغییر کرد", Severity.Success);
            await GetGridData();
        }
    }

    private async Task OpenInvoice(int orderId)
    {
        var parameters = new DialogParameters { { "OrderId", orderId } };
        var options = ConstantsClient.DialogOptionsMedium;
        var dialog = await dialogService.ShowAsync<Invoice>(string.Empty, parameters, options);
    }
}