namespace KomaeiSample.Client.Components;
public partial class PricesHandBag
{
    EnvelopeHandBagDto[] EnvelopeHandBagDtos = Array.Empty<EnvelopeHandBagDto>();
    [Parameter] public string? WindowHeight { get; set; }

    protected override async Task OnInitializedAsync()
    {
        EnvelopeHandBagDtos = (await http.GetFromJsonAsync<EnvelopeHandBagDto[]>("EnvelopeHandBag/GetAllEnables"))!;
    }

    private async Task DownloadPdf()
    {
        var pdfBytes = await http.GetByteArrayAsync($"EnvelopeHandBag/GetPricesPdf/");
        await jSRuntime.InvokeVoidAsync("downloadFile", $"KomaeiSample.ir_EnvelopeHandBagPrices.pdf", "application/pdf", pdfBytes);
    }
}