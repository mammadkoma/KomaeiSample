namespace KomaeiSample.Client.Components;
public partial class UpdateMultiplePrices
{
    [Parameter] public CategoriesEnum CategoriesEnum { get; set; }
    UpdateMultiplePricesVm vm { get; set; } = new();
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;

    protected override void OnInitialized()
    {
        vm.CategoriesEnum = CategoriesEnum;
    }

    private async Task OnValidSubmit()
    {
        if (vm.UpdateMultiplePriceType == UpdateMultiplePriceType.Price && (vm.Price == 0 || vm.Price == null))
        {
            snackbar.Add("قیمت افزایش را وارد کنید", Severity.Warning);
            return;
        }
        if (vm.UpdateMultiplePriceType == UpdateMultiplePriceType.Percent && (vm.Percent == 0 || vm.Percent == null))
        {
            snackbar.Add("درصد افزایش را وارد کنید", Severity.Warning);
            return;
        }
        var httpResponseMessage = await http.PostAsJsonAsync("Envelope/UpdateMultiplePrices", vm);
        if (httpResponseMessage.IsSuccessStatusCode)
            MudDialog.Close(DialogResult.Ok(true));
    }
}