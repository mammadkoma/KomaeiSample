namespace KomaeiSample.Client.Pages.Panel;
public partial class EnvelopeOfficeAddEdit
{
    private AddEditEnvelopeOfficeVm vm = new();
    [Parameter] public EnvelopeOfficeDto? Row { get; set; }
    [Parameter] public bool IsAdd { get; set; }
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;
    ModelDto[]? models = Array.Empty<ModelDto>();
    PaperDto[]? papers = Array.Empty<PaperDto>();
    GrammageDto[]? grammages = Array.Empty<GrammageDto>();
    CountDto[]? counts = Array.Empty<CountDto>();

    protected override async Task OnInitializedAsync()
    {
        var modelPaperGrammageCountDto = await http.GetFromJsonAsync<ModelPaperGrammageCountDto>("Share/GetAllModelPaperGrammageCountByCategoryId/1");
        models = modelPaperGrammageCountDto!.Models;
        papers = modelPaperGrammageCountDto!.Papers;
        grammages = modelPaperGrammageCountDto!.Grammages;
        counts = modelPaperGrammageCountDto!.Counts;

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
            var httpResponseMessage = await http.PostAsJsonAsync("EnvelopeOffice/Add", vm);
            if (httpResponseMessage.IsSuccessStatusCode)
                MudDialog.Close(DialogResult.Ok(true));
        }
        else // edit
        {
            var httpResponseMessage = await http.PostAsJsonAsync("EnvelopeOffice/Edit", vm);
            if (httpResponseMessage.IsSuccessStatusCode)
                MudDialog.Close(DialogResult.Ok(true));
        }
    }
}