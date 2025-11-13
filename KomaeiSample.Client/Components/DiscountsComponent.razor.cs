namespace KomaeiSample.Client.Components;
public partial class DiscountsComponent
{
    [Parameter] public bool IsAdmin { get; set; }

    DiscountDto[] DiscountDtos = Array.Empty<DiscountDto>();

    protected override async Task OnInitializedAsync()
    {
        await GetGridData();
    }

    private async Task GetGridData()
    {
        DiscountDtos = IsAdmin ? (await http.GetFromJsonAsync<DiscountDto[]>("Discount/GetAll"))! : (await http.GetFromJsonAsync<DiscountDto[]>("Discount/GetAllEnables"))!;
    }

    private async Task OpenAddEditDialog(DiscountDto? row, bool isAdd)
    {
        var parameters = new DialogParameters();
        if (row != null)
            parameters.Add("Row", row);
        parameters.Add("IsAdd", isAdd);
        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };
        var dialog = await dialogService.ShowAsync<DiscountAddEdit>(
            $"{(row == null ? "افزودن" : "ویرایش")} تخفیف", parameters, options);
        var dialogResult = await dialog.Result;
        if (dialogResult!.Canceled == false && dialogResult.Data is true)
            await GetGridData();
    }

    private Func<DiscountDto, int, string> _rowStyleFunc => (x, i) =>
    {
        if (x.Disable == true)
            return "background-color: #ff5e95;color: white;";
        return "";
    };
}