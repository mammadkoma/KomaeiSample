namespace KomaeiSample.Client.Components;
public partial class Invoice
{
    [Parameter] public int OrderId { get; set; }
    public InvoiceDto? InvoiceDto { get; set; }

    protected override async Task OnInitializedAsync()
    {
        InvoiceDto = await http.GetFromJsonAsync<InvoiceDto>("Order/GetInvoiceData/" + OrderId);
    }

    private async Task PrintInvoice()
    {
        var pdfBytes = await http.GetByteArrayAsync($"Order/ExportInvoicePdf/{OrderId}");
        await jSRuntime.InvokeVoidAsync("downloadFile", $"KomaeiSample.ir_Invoice_{OrderId}.pdf", "application/pdf", pdfBytes);
    }
}