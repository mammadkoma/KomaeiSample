namespace KomaeiSample.Client.Pages;
public partial class OrderAdd
{
    [Parameter] public required int CategoryId { get; set; }
    [Parameter] public required int ModelId { get; set; }
    EnvelopeOfficeDto[] EnvelopeOffices = [];
    EnvelopeHospitalDto[] EnvelopeHospitals = [];
    EnvelopeHandBagDto[] EnvelopeHandBags = [];
    EnvelopeConfidentialDto[] EnvelopeConfidentials = [];
    [CascadingParameter] public required MainLayout Layout { get; set; }
    private int _lastCategoryId;
    private int _lastModelId;

    protected override async Task OnParametersSetAsync()
    {
        if (CategoryId != _lastCategoryId || ModelId != _lastModelId)
        {
            _lastCategoryId = CategoryId;
            _lastModelId = ModelId;
            await LoadEnvelopeDataAsync();
            StateHasChanged();
        }
    }

    private async Task LoadEnvelopeDataAsync()
    {
        if (CategoryId == CategoriesEnum.Office.ToInt())
            EnvelopeOffices = (await http.GetFromJsonAsync<EnvelopeOfficeDto[]>("EnvelopeOffice/GetAllEnables"))!;
        else if (CategoryId == CategoriesEnum.Hospital.ToInt())
            EnvelopeHospitals = (await http.GetFromJsonAsync<EnvelopeHospitalDto[]>("EnvelopeHospital/GetAllEnables"))!;
        else if (CategoryId == CategoriesEnum.HandBag.ToInt())
            EnvelopeHandBags = (await http.GetFromJsonAsync<EnvelopeHandBagDto[]>("EnvelopeHandBag/GetAllEnables"))!;
        else if (CategoryId == CategoriesEnum.Confidential.ToInt())
            EnvelopeConfidentials = (await http.GetFromJsonAsync<EnvelopeConfidentialDto[]>("EnvelopeConfidential/GetAllEnables"))!;
    }
}