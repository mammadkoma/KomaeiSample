namespace KomaeiSample.Client.Pages;
public partial class UserAddresses
{
    UserAddressDto[] UserAddressDtos = Array.Empty<UserAddressDto>();
    string NoDataMsg = "";
    [Parameter] public bool SelectableMode { get; set; } = false;
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await GetData();
    }

    private async Task GetData()
    {
        UserAddressDtos = (await http.GetFromJsonAsync<UserAddressDto[]>("UserAddress/GetAll"))!;
        if (UserAddressDtos.Length == 0)
            NoDataMsg = "آدرسی ثبت نکرده اید";
    }

    private async Task OpenAddEditDialog(UserAddressDto row)
    {
        var parameters = new DialogParameters();
        if (row != null)
            parameters.Add("row", row);
        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, FullWidth = true, CloseOnEscapeKey = true };
        var dialog = await dialogService.ShowAsync<UserAddressAddEdit>("افزودن آدرس", parameters, options);
        var dialogResult = await dialog.Result;
        if (dialogResult!.Canceled == false && dialogResult.Data is true)
            await GetData();
    }

    private async Task Delete(UserAddressDto row)
    {
        var parameters = new DialogParameters<ConfirmDialog>
        {
            { x => x.ContentText, "مطمئن هستید؟" },
            { x => x.YesButtonText, "حذف" },
            { x => x.NoButtonText, "انصراف" },
            { x => x.Color, Color.Error }
        };
        var options = new DialogOptions { BackgroundClass = "backdrop-filter", CloseOnEscapeKey = true, CloseButton = true, FullWidth = true };
        var dialog = await dialogService.ShowAsync<ConfirmDialog>($"حذف آدرس : {row.Title}", parameters, options);
        var dialogResult = await dialog.Result;
        if (dialogResult!.Canceled == false && dialogResult.Data is true)
        {
            var httpResponseMessage = await http.DeleteAsync($"UserAddress/Delete?id={row.Id}");
            if (httpResponseMessage.IsSuccessStatusCode)
                await GetData();
        }
    }

    private void SelectAddress(UserAddressDto address)
    {
        MudDialog.Close(DialogResult.Ok(address.Address));
    }
}