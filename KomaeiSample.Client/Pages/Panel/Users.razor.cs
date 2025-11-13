namespace KomaeiSample.Client.Pages.Panel;
public partial class Users
{
    UserDto[] UserDtos = Array.Empty<UserDto>();

    protected override async Task OnInitializedAsync()
    {
        UserDtos = (await http.GetFromJsonAsync<UserDto[]>("User/GetAllConfirmed"))!;
    }

    private async Task OpenMyOrdersDialog(UserDto userDto)
    {
        var parameters = new DialogParameters { { nameof(MyOrders.UserId), userDto.Id } };
        var options = ConstantsClient.DialogOptionsLarge;
        var dialog = await dialogService.ShowAsync<MyOrders>("سفارشهای کاربر : " + userDto.FullName, parameters, options);
    }
}