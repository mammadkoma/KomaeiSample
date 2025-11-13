namespace KomaeiSample.Client.Pages.Panel;
public partial class EnvelopeHandBagAddEdit
{
    private AddEditEnvelopeHandBagVm vm = new();
    [Parameter] public EnvelopeHandBagDto? Row { get; set; }
    [Parameter] public bool IsAdd { get; set; }
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;
    ModelDto[]? models = Array.Empty<ModelDto>();
    PaperDto[]? papers = Array.Empty<PaperDto>();
    GrammageDto[]? grammages = Array.Empty<GrammageDto>();
    CountDto[]? counts = Array.Empty<CountDto>();
    CellophaneDto[]? cellophanes = Array.Empty<CellophaneDto>();
    UvDto[]? uvs = Array.Empty<UvDto>();

    protected override async Task OnInitializedAsync()
    {
        var modelPaperGrammageCountDto = await http.GetFromJsonAsync<ModelPaperGrammageCountCellophaneUvDto>("Share/GetAllModelPaperGrammageCountCellophaneUvDtoByCategoryId/" + CategoriesEnum.HandBag.ToInt());
        models = modelPaperGrammageCountDto!.Models;
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
            var httpResponseMessage = await http.PostAsJsonAsync("EnvelopeHandBag/Add", vm);
            if (httpResponseMessage.IsSuccessStatusCode)
                MudDialog.Close(DialogResult.Ok(true));
        }
        else // edit
        {
            var httpResponseMessage = await http.PostAsJsonAsync("EnvelopeHandBag/Edit", vm);
            if (httpResponseMessage.IsSuccessStatusCode)
                MudDialog.Close(DialogResult.Ok(true));
        }
    }
}