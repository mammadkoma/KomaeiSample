namespace KomaeiSample.Client.Components;
public partial class PricesOffice
{
    EnvelopeOfficeDto[] EnvelopeOfficeDtos = Array.Empty<EnvelopeOfficeDto>();
    [Parameter] public string? WindowHeight { get; set; }

    protected override async Task OnInitializedAsync()
    {
        EnvelopeOfficeDtos = (await http.GetFromJsonAsync<EnvelopeOfficeDto[]>("EnvelopeOffice/GetAllEnables"))!;
    }

    private async Task DownloadPdf()
    {
        var pdfBytes = await http.GetByteArrayAsync($"EnvelopeOffice/GetPricesPdf/");
        await jSRuntime.InvokeVoidAsync("downloadFile", $"KomaeiSample.ir_EnvelopeOfficePrices.pdf", "application/pdf", pdfBytes);
    }
}