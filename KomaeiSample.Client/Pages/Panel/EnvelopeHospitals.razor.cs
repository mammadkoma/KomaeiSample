namespace KomaeiSample.Client.Pages.Panel;
public partial class EnvelopeHospitals
{
    EnvelopeHospitalDto[] EnvelopeHospitalDtos = Array.Empty<EnvelopeHospitalDto>();

    protected override async Task OnInitializedAsync()
    {
        await GetGridData();
    }

    private async Task GetGridData()
    {
        EnvelopeHospitalDtos = (await http.GetFromJsonAsync<EnvelopeHospitalDto[]>("EnvelopeHospital/GetAll"))!;
    }

    private async Task OpenAddEditDialog(EnvelopeHospitalDto? row, bool isAdd)
    {
        var parameters = new DialogParameters();
        if (row != null)
            parameters.Add("row", row);
        parameters.Add("IsAdd", isAdd);
        var options = ConstantsClient.DialogOptionsLarge;
        var dialog = await dialogService.ShowAsync<EnvelopeHospitalAddEdit>(
            $"{(row == null ? "افزودن" : "ویرایش")} پاکت آزمایشگاهی و بیمارستانی", parameters, options);
        var dialogResult = await dialog.Result;
        if (dialogResult!.Canceled == false && dialogResult.Data is true)
            await GetGridData();
    }

    private Func<EnvelopeHospitalDto, int, string> _rowStyleFunc => (x, i) =>
    {
        if (x.Disable == true)
            return "background-color: #ff5e95;color: white;";
        return "";
    };

    private async Task OpenUpdateMultiplePricesDialog()
    {
        var parameters = new DialogParameters();
        parameters.Add("CategoriesEnum", CategoriesEnum.Hospital);
        var options = ConstantsClient.DialogOptionsSmall;
        var dialog = await dialogService.ShowAsync<UpdateMultiplePrices>(
            $"ویرایش دسته ای قیمت : {CategoriesEnum.Hospital.GetDescription()}", parameters, options);
        var dialogResult = await dialog.Result;
        if (dialogResult!.Canceled == false && dialogResult.Data is true)
        {
            snackbar.Add("با موفقیت انجام شد", Severity.Success);
            await GetGridData();
        }
    }
}