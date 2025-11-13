namespace KomaeiSample.Client.Components;
public partial class PricesHospital
{
    EnvelopeHospitalDto[] EnvelopeHospitalDtos = Array.Empty<EnvelopeHospitalDto>();
    [Parameter] public string? WindowHeight { get; set; }

    protected override async Task OnInitializedAsync()
    {
        EnvelopeHospitalDtos = (await http.GetFromJsonAsync<EnvelopeHospitalDto[]>("EnvelopeHospital/GetAllEnables"))!;
    }

    private async Task DownloadPdf()
    {
        var pdfBytes = await http.GetByteArrayAsync($"EnvelopeHospital/GetPricesPdf/");
        await jSRuntime.InvokeVoidAsync("downloadFile", $"KomaeiSample.ir_EnvelopeHospitalPrices.pdf", "application/pdf", pdfBytes);
    }
}