namespace KomaeiSample.Client.Pages.Panel;
public partial class OrderEdit
{
    [Parameter] public OrderEditVm vm { get; set; } = new();
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;


    private async Task OnValidSubmit()
    {
        var httpResponseMessage = await http.PostAsJsonAsync("Order/Edit", vm);
        if (httpResponseMessage.IsSuccessStatusCode)
            MudDialog.Close(DialogResult.Ok(true));
    }
}