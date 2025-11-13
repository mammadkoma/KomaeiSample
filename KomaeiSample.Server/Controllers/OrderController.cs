namespace KomaeiSample.Server.Controllers;
public class OrderController(OrderService orderService, IHttpContextAccessor httpContext) : AppController
{
    [HttpGet]
    public async Task<ActionResult> GetAllForCardOfOneUser()
    {
        return Ok(await orderService.GetAll(httpContext.GetUserId(), isForCard: true));
    }

    [HttpGet]
    public async Task<ActionResult> GetAllOfOneUser()
    {
        return Ok(await orderService.GetAll(httpContext.GetUserId(), isForCard: false));
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult> GetAllOfOneUserForAdmin(Guid userId)
    {
        return Ok(await orderService.GetAll(userId, isForCard: false));
    }

    [HttpGet]
    [Authorize(Roles = $"{Constants.AdminRole},{Constants.SuperAdminRole}")]
    public async Task<ActionResult> GetAll()
    {
        return Ok(await orderService.GetAll(addUserId: null, isForCard: false));
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult> GetInvoiceData(int Id)
    {
        return Ok(await orderService.GetInvoiceData(Id));
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromForm] OrderVm vm, [FromForm] IFormFile file)
    {
        return Ok(await orderService.Add(vm, file));
    }

    [HttpPost]
    [Authorize(Roles = $"{Constants.AdminRole},{Constants.SuperAdminRole}")]
    public async Task<ActionResult> Edit(OrderEditVm vm)
    {
        return Ok(await orderService.Edit(vm));
    }

    [HttpPost] // in card
    public async Task<ActionResult> ConfirmPayAndSendToMyOrdersBatch(ConfirmPayRequestVm vm)
    {
        return Ok(await orderService.ConfirmPayAndSendToMyOrdersBatch(vm));
    }

    [HttpPost("{id:int}")] // in card
    public async Task<ActionResult> Delete(int id)
    {
        return Ok(await orderService.Delete(id));
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> ExportInvoicePdf(int orderId)
    {
        var invoice = await orderService.GetInvoiceData(orderId);
        if (invoice == null)
            return NotFound();
        QuestPDF.Settings.License = LicenseType.Community;
        var fontPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "YekanBakhFaNum-Regular.ttf");
        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "favicon.png");
        var imagePathFooter = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "InvoiceFooter.png");
        FontManager.RegisterFont(System.IO.File.OpenRead(fontPath));
        var pdfBytes = Document.Create(container =>
        {
            byte[] imageData = System.IO.File.ReadAllBytes(imagePath);
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Content().PaddingHorizontal(40).Column(col =>
                {
                    col.Item().PaddingTop(30);
                    col.Item().AlignCenter().Width(200).Image(imageData).WithCompressionQuality(ImageCompressionQuality.Best);
                    col.Item().Text("از اعتماد شما سپاسگزاریم").AlignCenter().FontColor("#1E6060").FontFamily("Yekan Bakh FaNum").FontSize(14);
                    col.Item().PaddingVertical(15).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                        });
                        void AddRow(int rowIndex, string label, string? value)
                        {
                            var backgroundColor = rowIndex % 2 != 0 ? Colors.Grey.Lighten3 : Colors.White;

                            table.Cell().Background(backgroundColor).Border(1).BorderColor(Colors.Grey.Lighten2)
                                .Padding(5).AlignMiddle().AlignCenter()
                                .Text(value ?? "").FontFamily("Yekan Bakh FaNum").FontSize(12);

                            table.Cell().Background(backgroundColor).Border(1).BorderColor(Colors.Grey.Lighten2)
                                .Padding(5).AlignMiddle().AlignCenter()
                                .Text(label).FontFamily("Yekan Bakh FaNum").FontSize(12);
                        }
                        int row = 0;
                        AddRow(row++, "شماره فاکتور", invoice.Id.ToString());
                        AddRow(row++, "نام مشتری", invoice.AddUserFullName);
                        AddRow(row++, "دسته", invoice.CategoryTitle);
                        AddRow(row++, invoice.CategoryTitle == CategoriesEnum.HandBag.GetDescription() ? "سایز" : "مدل", invoice.ModelTitle);
                        if (invoice.CategoryTitle == CategoriesEnum.Hospital.GetDescription())
                            AddRow(row++, "قالب", invoice.EnvelopeHospitalTemplateName!.ToString());
                        AddRow(row++, invoice.CategoryTitle == CategoriesEnum.Confidential.GetDescription() ? "نوع" : "کاغذ", invoice.PaperTitle);
                        AddRow(row++, "گرماژ", invoice.GrammageTitle);
                        AddRow(row++, "تعداد", invoice.CountTitle);
                        string rtlMark = "\u200F";
                        AddRow(row++, "قیمت", $"{rtlMark}{invoice.Price:N0} ریال");
                        AddRow(row++, "قیمت پس از تخفیف", invoice.PriceAfterDiscount != null ? $"{rtlMark}{invoice.PriceAfterDiscount:N0} ریال" : "");
                        AddRow(row++, "تاریخ سفارش", invoice.AddDate.ToShamsiDate());
                        AddRow(row++, "تاریخ پرداخت", invoice.PayDate?.ToShamsiDate());
                    });
                });
                page.Footer().PaddingHorizontal(0).Image(imagePathFooter).WithCompressionQuality(ImageCompressionQuality.Best);
            });
        }).GeneratePdf();
        return File(pdfBytes, "application/pdf", $"فاکتور_{orderId}.pdf");
    }
}