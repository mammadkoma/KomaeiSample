namespace KomaeiSample.Client.Pages.Panel;
public partial class EnvelopeConfidentials
{
    EnvelopeConfidentialDto[] EnvelopeConfidentialDtos = Array.Empty<EnvelopeConfidentialDto>();

    protected override async Task OnInitializedAsync()
    {
        await GetGridData();
    }

    private async Task GetGridData()
    {
        EnvelopeConfidentialDtos = (await http.GetFromJsonAsync<EnvelopeConfidentialDto[]>("EnvelopeConfidential/GetAll"))!;
    }

    private async Task OpenAddEditDialog(EnvelopeConfidentialDto? row, bool isAdd)
    {
        var parameters = new DialogParameters();
        if (row != null)
            parameters.Add("row", row);
        parameters.Add("IsAdd", isAdd);
        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };
        var dialog = await dialogService.ShowAsync<EnvelopeConfidentialAddEdit>(
            $"{(row == null ? "افزودن" : "ویرایش")} {CategoriesEnum.Confidential.GetDescription()}", parameters, options);
        var dialogResult = await dialog.Result;
        if (dialogResult!.Canceled == false && dialogResult.Data is true)
            await GetGridData();
    }

    private Func<EnvelopeConfidentialDto, int, string> _rowStyleFunc => (x, i) =>
    {
        if (x.Disable == true)
            return "background-color: #ff5e95;color: white;";
        return "";
    };

    private async Task OpenUpdateMultiplePricesDialog()
    {
        var parameters = new DialogParameters();
        parameters.Add("CategoriesEnum", CategoriesEnum.Confidential);
        var options = ConstantsClient.DialogOptionsSmall;
        var dialog = await dialogService.ShowAsync<UpdateMultiplePrices>(
            $"ویرایش دسته ای قیمت : {CategoriesEnum.Confidential.GetDescription()}", parameters, options);
        var dialogResult = await dialog.Result;
        if (dialogResult!.Canceled == false && dialogResult.Data is true)
        {
            snackbar.Add("با موفقیت انجام شد", Severity.Success);
            await GetGridData();
        }
    }
}