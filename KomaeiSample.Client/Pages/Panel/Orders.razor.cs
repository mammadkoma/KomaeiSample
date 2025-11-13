namespace KomaeiSample.Client.Pages.Panel;
public partial class Orders
{
    OrderDto OrderDto = new();
    OrderOfficeDto[] OrdersOffice = Array.Empty<OrderOfficeDto>();
    OrderHospitalDto[] OrdersHospital = Array.Empty<OrderHospitalDto>();
    OrderHandBagDto[] OrdersHandBag = Array.Empty<OrderHandBagDto>();
    OrderConfidentialDto[] OrdersConfidential = Array.Empty<OrderConfidentialDto>();
    string NoDataMsg = "";

    protected override async Task OnInitializedAsync()
    {
        await GetGridData();
    }

    private async Task GetGridData()
    {
        OrderDto = (await http.GetFromJsonAsync<OrderDto>("Order/GetAll"))!;
        OrdersOffice = OrderDto.OrderOfficeDtos!;
        OrdersHospital = OrderDto.OrderHospitalDtos!;
        OrdersHandBag = OrderDto.OrderHandBagDtos!;
        OrdersConfidential = OrderDto.OrderConfidentialDtos!;
        if (OrdersOffice.Length == 0 && OrdersHospital.Length == 0 && OrdersHandBag.Length == 0 && OrdersConfidential.Length == 0)
            NoDataMsg = "سفارشی وجود ندارد";
    }
}