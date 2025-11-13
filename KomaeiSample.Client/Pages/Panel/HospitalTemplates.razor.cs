namespace KomaeiSample.Client.Pages.Panel;
public partial class HospitalTemplates
{
    HospitalTemplateDto[] HospitalTemplateDtos = Array.Empty<HospitalTemplateDto>();
    ModelDto[] ModelDtos = Array.Empty<ModelDto>();
    private int? ModelId;
    string NoRecordMsg = "";

    protected override async Task OnInitializedAsync()
    {
        ModelDtos = (await http.GetFromJsonAsync<ModelDto[]>("Model/GetAllByCategoryId/" + CategoriesEnum.Hospital.ToInt()))!;
    }

    private async Task GetData()
    {
        HospitalTemplateDtos = (await http.GetFromJsonAsync<HospitalTemplateDto[]>("HospitalTemplate/GetAllByModelId/" + ModelId))!;
        if (HospitalTemplateDtos.Length == 0)
        {
            NoRecordMsg = "هیچ قالبی برای این مدل وجود ندارد";
        }
        //else
        //{
        //    NoRecordMsg = "";
        //}
    }

    private async Task OnModelChangeAsync(int? newValue)
    {
        ModelId = newValue!.Value;

        if (ModelId.HasValue)
            await GetData();
    }

    private async Task OpenAddDialog(HospitalTemplateDto? row)
    {
        var parameters = new DialogParameters();
        if (row != null)
            parameters.Add("row", row);
        parameters.Add("ModelId", ModelId);
        var options = ConstantsClient.DialogOptionsSmall;
        var dialog = await dialogService.ShowAsync<HospitalTemplateAdd>(
            $"افزودن قالب آزمایشگاهی و بیمارستانی", parameters, options);
        var dialogResult = await dialog.Result;
        if (dialogResult!.Canceled == false && dialogResult.Data is true)
            await GetData();
    }

    private async Task DeleteRow(HospitalTemplateDto row)
    {
        var parameters = new DialogParameters<ConfirmDialog>
        {
            { x => x.ContentText, "مطمئن هستید میخواهید این قالب را حذف کنید؟" },
            { x => x.YesButtonText, "حذف" },
            { x => x.NoButtonText, "انصراف" },
            { x => x.Color, Color.Error }
        };
        var options = ConstantsClient.DialogOptionsSmall;
        var dialog = await dialogService.ShowAsync<ConfirmDialog>($"حذف قالب : {row.TemplateName}", parameters, options);
        var dialogResult = await dialog.Result;
        if (dialogResult!.Canceled == false && dialogResult.Data is true)
        {
            var response = await http.PostAsync("HospitalTemplate/Delete/" + row.Id, null);
            if (response.IsSuccessStatusCode)
            {
                snackbar.Add($"با موفقیت حذف شد", Severity.Success);
                await GetData();
            }
        }
    }

    public async Task GoDown(int id)
    {
        var response = await http.PostAsync("HospitalTemplate/GoDown/" + id, null);
        if (response.IsSuccessStatusCode)
            await GetData();
    }

    public async Task GoUp(int id)
    {
        var response = await http.PostAsync("HospitalTemplate/GoUp/" + id, null);
        if (response.IsSuccessStatusCode)
            await GetData();
    }
}