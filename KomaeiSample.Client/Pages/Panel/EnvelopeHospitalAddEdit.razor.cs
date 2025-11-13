namespace KomaeiSample.Client.Pages.Panel;
public partial class EnvelopeHospitalAddEdit
{
    private AddEditEnvelopeHospitalVm vm = new();
    [Parameter] public EnvelopeHospitalDto? Row { get; set; }
    [Parameter] public bool IsAdd { get; set; }
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;
    ModelDto[]? models = Array.Empty<ModelDto>();
    HospitalTemplateDto[]? hospitalTemplates = Array.Empty<HospitalTemplateDto>();
    HospitalTemplateDto[]? hospitalTemplatesForShow = Array.Empty<HospitalTemplateDto>();
    PaperDto[]? papers = Array.Empty<PaperDto>();
    GrammageDto[]? grammages = Array.Empty<GrammageDto>();
    CountDto[]? counts = Array.Empty<CountDto>();
    CellophaneDto[]? cellophanes = Array.Empty<CellophaneDto>();
    UvDto[]? uvs = Array.Empty<UvDto>();

    protected override async Task OnInitializedAsync()
    {
        var modelPaperGrammageCountDto = await http.GetFromJsonAsync<EnvelopeHospitalAddEditPageDto>("Share/GetAddEditEnvelopeHospitalSelectDataByCategoryId/" + CategoriesEnum.Hospital.ToInt());
        models = modelPaperGrammageCountDto!.Models;
        hospitalTemplates = modelPaperGrammageCountDto!.HospitalTemplates;
        papers = modelPaperGrammageCountDto!.Papers;
        grammages = modelPaperGrammageCountDto!.Grammages;
        counts = modelPaperGrammageCountDto!.Counts;
        cellophanes = modelPaperGrammageCountDto.Cellophanes;
        uvs = modelPaperGrammageCountDto.Uvs;

        if (Row != null)
        {
            Row.Adapt(vm);
            StateHasChanged();
        }
    }

    private async Task OnValidSubmit()
    {
        if (Row == null) // add
        {
            if (vm.HospitalTemplateId == null)
            {
                snackbar.Add("قالب را انتخاب کنید", Severity.Warning);
                return;
            }
            var httpResponseMessage = await http.PostAsJsonAsync("EnvelopeHospital/Add", vm);
            if (httpResponseMessage.IsSuccessStatusCode)
                MudDialog.Close(DialogResult.Ok(true));
        }
        else // edit
        {
            var httpResponseMessage = await http.PostAsJsonAsync("EnvelopeHospital/Edit", vm);
            if (httpResponseMessage.IsSuccessStatusCode)
                MudDialog.Close(DialogResult.Ok(true));
        }
    }

    private string GetCardStyle(int id)
    {
        return vm.HospitalTemplateId == id
            ? "border: 2px solid #1E6060; box-shadow: 0 0 10px #1976d2;"
            : "border: 1px solid #ccc;";
    }

    private void OnModelChange()
    {
        vm.HospitalTemplateId = null;
        hospitalTemplatesForShow = hospitalTemplates!.Where(x => x.ModelId == vm.ModelId).ToArray();
    }

    private async Task OpenShowImageDialog(HospitalTemplateDto? row)
    {
        var parameters = new DialogParameters();
        if (row != null)
            parameters.Add("Src", "Uploads/HospitalTemplate/" + row.FileName);
        var options = ConstantsClient.DialogOptionsMedium;
        var dialog = await dialogService.ShowAsync<ShowImageComponent>(
            $"{row!.TemplateName}", parameters, options);
    }
}