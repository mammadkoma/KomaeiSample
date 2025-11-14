namespace KomaeiSample.Client.Pages;

public partial class CategoryAddEdit
{
    CategoryAddEditVm vm = new();
    [Parameter] public CategoryDto? Row { get; set; }
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        if (Row != null)
        {
            Row.Adapt(vm);
            StateHasChanged();
        }
        //await base.OnInitializedAsync();
    }

    private async Task OnValidSubmit()
    {
        if (Row == null) // add
        {
            var httpResponseMessage = await http.PostAsJsonAsync("Category/Add", vm);
            if (httpResponseMessage.IsSuccessStatusCode)
                MudDialog.Close(DialogResult.Ok(true));
        }
        else // edit
        {
            var httpResponseMessage = await http.PostAsJsonAsync("Category/Edit", vm);
            if (httpResponseMessage.IsSuccessStatusCode)
                MudDialog.Close(DialogResult.Ok(true));
        }
    }
}