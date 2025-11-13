namespace KomaeiSample.Client.Pages;
public partial class UserAddressAddEdit
{
    UserAddressAddEditVm vm = new();
    [Parameter] public UserAddressDto? Row { get; set; }
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        if (Row != null)
        {
            Row.Adapt(vm);
            StateHasChanged();
        }
        await base.OnInitializedAsync();
    }

    private async Task OnValidSubmit()
    {
        if (Row == null) // add
        {
            var httpResponseMessage = await http.PostAsJsonAsync("UserAddress/Add", vm);
            if (httpResponseMessage.IsSuccessStatusCode)
                MudDialog.Close(DialogResult.Ok(true));
        }
        else // edit
        {
            var httpResponseMessage = await http.PostAsJsonAsync("UserAddress/Edit", vm);
            if (httpResponseMessage.IsSuccessStatusCode)
                MudDialog.Close(DialogResult.Ok(true));
        }
    }
}