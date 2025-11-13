namespace KomaeiSample.Client.Components;
public partial class PricesConfidential
{
    EnvelopeConfidentialDto[] EnvelopeConfidentialDtos = Array.Empty<EnvelopeConfidentialDto>();
    [Parameter] public string? WindowHeight { get; set; }

    protected override async Task OnInitializedAsync()
    {
        EnvelopeConfidentialDtos = (await http.GetFromJsonAsync<EnvelopeConfidentialDto[]>("EnvelopeConfidential/GetAllEnables"))!;
    }

    private async Task DownloadPdf()
    {
        var pdfBytes = await http.GetByteArrayAsync($"EnvelopeConfidential/GetPricesPdf/");
        await jSRuntime.InvokeVoidAsync("downloadFile", $"KomaeiSample.ir_EnvelopeConfidentialPrices.pdf", "application/pdf", pdfBytes);
    }
}