namespace KomaeiSample.Client.Pages;
public partial class Card
{
    OrderDto _orderDto = new();
    private HashSet<OrderOfficeDto> SelectedOrderOffices = new();
    private HashSet<OrderHospitalDto> SelectedOrderHospitals = new();
    private HashSet<OrderHandBagDto> SelectedOrderHandBags = new();
    private HashSet<OrderConfidentialDto> SelectedOrderConfidentials = new();
    string NoDataMsg = "";
    bool _isCalcByWalletBalance = false;

    protected override async Task OnInitializedAsync()
    {
        await GetGridData();
    }

    private async Task GetGridData()
    {
        _orderDto = (await http.GetFromJsonAsync<OrderDto>("Order/GetAllForCardOfOneUser"))!;
        if (_orderDto.OrderOfficeDtos?.Length == 0 && _orderDto.OrderHospitalDtos?.Length == 0 && _orderDto.OrderHandBagDtos?.Length == 0 && _orderDto.OrderConfidentialDtos?.Length == 0)
            NoDataMsg = "موردی در سبد خرید شما وجود ندارد";
    }

    private async Task PayButtonClick()
    {
        decimal PriceTotal = 0;
        List<int> orderIds = new();

        foreach (var item in SelectedOrderOffices)
        {
            PriceTotal += (item.PriceAfterDiscount == null ? item.Price : item.PriceAfterDiscount.Value);
            orderIds.Add(item.Id);
        }

        foreach (var item in SelectedOrderHospitals)
        {
            PriceTotal += (item.PriceAfterDiscount == null ? item.Price : item.PriceAfterDiscount.Value);
            orderIds.Add(item.Id);
        }

        foreach (var item in SelectedOrderHandBags)
        {
            PriceTotal += (item.PriceAfterDiscount == null ? item.Price : item.PriceAfterDiscount.Value);
            orderIds.Add(item.Id);
        }

        foreach (var item in SelectedOrderConfidentials)
        {
            PriceTotal += (item.PriceAfterDiscount == null ? item.Price : item.PriceAfterDiscount.Value);
            orderIds.Add(item.Id);
        }

        var mobile = (await http.GetStringAsync("User/GetCurrentUserMobile"))!;

        var data = new
        {
            merchant = Constants.Merchant,
            amount = PriceTotal,
            callbackUrl = $"{navigationManager.BaseUri}UserPayCallback/{string.Join("-", orderIds)}",
            mobile = mobile
        };
        var response = await http.PostAsJsonAsync("https://gateway.zibal.ir/v1/request", data);
        string json = await response.Content.ReadAsStringAsync();
        var zibalResponse = JsonSerializer.Deserialize<ZibalResponse>(json);
        if (zibalResponse!.result != 100)
            snackbar.Add("خطا در برقراری ارتباط با درگاه پرداخت", Severity.Error);
        navigationManager.NavigateTo($"https://gateway.zibal.ir/start/{zibalResponse.trackId}");
    }

    private async Task DeleteRow(int id)
    {
        var parameters = new DialogParameters<ConfirmDialog>
        {
            { x => x.ContentText, "مطمئن هستید میخواهید این سفارش را حذف کنید؟" },
            { x => x.YesButtonText, "حذف" },
            { x => x.NoButtonText, "انصراف" },
            { x => x.Color, Color.Error }
        };
        var options = new DialogOptions { BackgroundClass = "backdrop-filter", CloseOnEscapeKey = true, CloseButton = true, FullWidth = true };
        var dialog = await dialogService.ShowAsync<ConfirmDialog>($"حذف سفارش با شماره فاکتور : {id}", parameters, options);
        var dialogResult = await dialog.Result;
        if (dialogResult!.Canceled == false && dialogResult.Data is true)
        {
            var response = await http.PostAsync("Order/Delete/" + id, null);
            if (response.IsSuccessStatusCode)
            {
                snackbar.Add($"با موفقیت حذف شد", Severity.Success);
                await GetGridData();
            }
        }
    }
}