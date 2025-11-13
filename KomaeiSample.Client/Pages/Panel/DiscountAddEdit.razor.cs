namespace KomaeiSample.Client.Pages.Panel;
public partial class DiscountAddEdit
{
    private AddEditDiscountVm vm = new();
    [Parameter] public DiscountDto? Row { get; set; }
    [Parameter] public bool IsAdd { get; set; }
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;
    CategoryDto[]? categoryDtos = Array.Empty<CategoryDto>();

    protected override async Task OnInitializedAsync()
    {
        categoryDtos = await http.GetFromJsonAsync<CategoryDto[]>("Category/GetAll");

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
            var httpResponseMessage = await http.PostAsJsonAsync("Discount/Add", vm);
            if (httpResponseMessage.IsSuccessStatusCode)
                MudDialog.Close(DialogResult.Ok(true));
        }
        else // edit
        {
            var httpResponseMessage = await http.PostAsJsonAsync("Discount/Edit", vm);
            if (httpResponseMessage.IsSuccessStatusCode)
                MudDialog.Close(DialogResult.Ok(true));
        }
    }
}