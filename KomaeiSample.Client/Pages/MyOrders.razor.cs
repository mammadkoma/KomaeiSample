namespace KomaeiSample.Client.Pages;
public partial class MyOrders
{
    OrderDto OrderDto = new();
    [Parameter] public Guid? UserId { get; set; }
    string NoDataMsg = "";

    protected override async Task OnInitializedAsync()
    {
        if (UserId != null)
            OrderDto = (await http.GetFromJsonAsync<OrderDto>("Order/GetAllOfOneUserForAdmin/" + UserId))!;
        else OrderDto = (await http.GetFromJsonAsync<OrderDto>("Order/GetAllOfOneUser"))!;
        if (OrderDto.OrderOfficeDtos?.Length == 0 && OrderDto.OrderHospitalDtos?.Length == 0 && OrderDto.OrderHandBagDtos?.Length == 0 && OrderDto.OrderConfidentialDtos?.Length == 0)
            NoDataMsg = "سفارشی وجود ندارد";
    }
}