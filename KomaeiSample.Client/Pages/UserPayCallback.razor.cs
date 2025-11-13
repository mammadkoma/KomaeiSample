namespace KomaeiSample.Client.Pages;
public partial class UserPayCallback
{
    [Parameter] public required string OrderIds { get; set; }
    [SupplyParameterFromQuery] private string? success { get; set; }
    [SupplyParameterFromQuery] private string? status { get; set; }
    [SupplyParameterFromQuery] private string? trackId { get; set; }
    public string Msg { get; set; } = "در حال بررسی پرداخت ، لطفا منتظر باشید ...";

    protected override async Task OnInitializedAsync()
    {
        if (success == "1")
        {
            var data = new
            {
                merchant = Constants.Merchant,
                trackId = trackId,
            };
            var response = await http.PostAsJsonAsync("https://gateway.zibal.ir/v1/verify", data);
            string json = await response.Content.ReadAsStringAsync();
            var zibalVerifyResponse = JsonSerializer.Deserialize<ZibalVerifyResponse>(json);
            if (zibalVerifyResponse!.result == 100) // success
            {
                Msg = "در حال انتقال به صفحه سفارش های من ...";
                StateHasChanged();
                long.TryParse(zibalVerifyResponse.refNumber!, out var refNum);
                var req = new ConfirmPayRequestVm
                {
                    Ids = OrderIds.Split("-").Select(int.Parse).ToList(),
                    RefNumber = refNum,
                    paidAt = zibalVerifyResponse.paidAt,
                };
                var response2 = await http.PostAsJsonAsync("Order/ConfirmPayAndSendToMyOrdersBatch", req);
                if (response2.IsSuccessStatusCode)
                {
                    snackbar.Add("پرداخت شما تایید شد", Severity.Success);
                    await Task.Delay(4000);
                    navigationManager.NavigateTo("/MyOrders");
                }
            }
            else
            {
                snackbar.Add("متاسفانه پرداخت شما توسط درگاه پرداخت تایید نشد ، لطفا به صفحه سبد خرید مراجعه کرده و مجددا پرداخت نمایید", Severity.Error);
            }
        }
        else
        {
            snackbar.Add("پرداخت انجام نشد", Severity.Error);
            await Task.Delay(4000);
            navigationManager.NavigateTo("/Card");
        }
    }
}