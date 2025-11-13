namespace KomaeiSample.Client.Pages.Panel;
public partial class EnvelopeOffices
{
    EnvelopeOfficeDto[] EnvelopeOfficeDtos = Array.Empty<EnvelopeOfficeDto>();

    protected override async Task OnInitializedAsync()
    {
        await GetGridData();
    }

    private async Task GetGridData()
    {
        EnvelopeOfficeDtos = (await http.GetFromJsonAsync<EnvelopeOfficeDto[]>("EnvelopeOffice/GetAll"))!;
    }

    private async Task OpenAddEditDialog(EnvelopeOfficeDto? row, bool isAdd)
    {
        var parameters = new DialogParameters();
        if (row != null)
            parameters.Add("row", row);
        parameters.Add("IsAdd", isAdd);
        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };
        var dialog = await dialogService.ShowAsync<EnvelopeOfficeAddEdit>(
            $"{(row == null ? "افزودن" : "ویرایش")} پاکت نامه و اداری", parameters, options);
        var dialogResult = await dialog.Result;
        if (dialogResult!.Canceled == false && dialogResult.Data is true)
            await GetGridData();
    }

    private Func<EnvelopeOfficeDto, int, string> _rowStyleFunc => (x, i) =>
    {
        if (x.Disable == true)
            return "background-color: #ff5e95;color: white;";
        return "";
    };

    private async Task OpenUpdateMultiplePricesDialog()
    {
        var parameters = new DialogParameters();
        parameters.Add("CategoriesEnum", CategoriesEnum.Office);
        var options = ConstantsClient.DialogOptionsSmall;
        var dialog = await dialogService.ShowAsync<UpdateMultiplePrices>(
            $"ویرایش دسته ای قیمت : {CategoriesEnum.Office.GetDescription()}", parameters, options);
        var dialogResult = await dialog.Result;
        if (dialogResult!.Canceled == false && dialogResult.Data is true)
        {
            snackbar.Add("با موفقیت انجام شد", Severity.Success);
            await GetGridData();
        }
    }
}