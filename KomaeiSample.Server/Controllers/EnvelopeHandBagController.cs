namespace KomaeiSample.Server.Controllers;
public class EnvelopeHandBagController(EnvelopeHandBagService envelopeHandBagService) : AppController
{
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return Ok(await envelopeHandBagService.GetAll());
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult> GetAllEnables()
    {
        return Ok(await envelopeHandBagService.GetAllEnables());
    }

    [HttpPost]
    [Authorize(Roles = $"{Constants.AdminRole},{Constants.SuperAdminRole}")]
    public async Task<ActionResult> Add(AddEditEnvelopeHandBagVm vm)
    {
        return Ok(await envelopeHandBagService.Add(vm));
    }

    [HttpPost]
    [Authorize(Roles = $"{Constants.AdminRole},{Constants.SuperAdminRole}")]
    public async Task<ActionResult> Edit(AddEditEnvelopeHandBagVm vm)
    {
        return Ok(await envelopeHandBagService.Edit(vm));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetPricesPdf()
    {
        var records = await envelopeHandBagService.GetAllEnables();
        QuestPDF.Settings.License = LicenseType.Community;
        var fontPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "YekanBakhFaNum-Regular.ttf");
        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "favicon.png");
        FontManager.RegisterFont(System.IO.File.OpenRead(fontPath));
        var pdfBytes = Document.Create(container =>
        {
            byte[] imageData = System.IO.File.ReadAllBytes(imagePath);
            container.Page(page =>
            {
                page.Size(PageSizes.A4.Landscape());
                page.ContentFromRightToLeft();
                page.Content().PaddingHorizontal(40).Column(col =>
                {
                    col.Item().PaddingTop(30);
                    col.Item().AlignCenter().Width(200).Image(imageData).WithCompressionQuality(ImageCompressionQuality.Best);
                    col.Item().Text($"قیمت های {CategoriesEnum.HandBag.GetDescription()}").AlignCenter().FontColor("#1E6060").FontFamily("Yekan Bakh FaNum").FontSize(14);
                    col.Item().PaddingVertical(15).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Border(1).AlignCenter().Text("سایز").FontFamily("Yekan Bakh FaNum").FontSize(12).Bold();
                            header.Cell().Border(1).AlignCenter().Text("کاغذ").FontFamily("Yekan Bakh FaNum").FontSize(12).Bold();
                            header.Cell().Border(1).AlignCenter().Text("گرماژ").FontFamily("Yekan Bakh FaNum").FontSize(12).Bold();
                            header.Cell().Border(1).AlignCenter().Text("تعداد").FontFamily("Yekan Bakh FaNum").FontSize(12).Bold();
                            header.Cell().Border(1).AlignCenter().Text("سلفون").FontFamily("Yekan Bakh FaNum").FontSize(12).Bold();
                            header.Cell().Border(1).AlignCenter().Text("یو وی").FontFamily("Yekan Bakh FaNum").FontSize(12).Bold();
                            header.Cell().Border(1).AlignCenter().Text("قیمت (ریال)").FontFamily("Yekan Bakh FaNum").FontSize(12).Bold();
                        });

                        for (int i = 0; i < records.Length; i++)
                        {
                            var item = records[i];
                            var background = i % 2 == 0 ? Colors.Grey.Lighten3 : Colors.Transparent;
                            table.Cell().Background(background).Border(1).AlignCenter().Text(item.ModelTitle).FontFamily("Yekan Bakh FaNum");
                            table.Cell().Background(background).Border(1).AlignCenter().Text(item.PaperTitle).FontFamily("Yekan Bakh FaNum");
                            table.Cell().Background(background).Border(1).AlignCenter().Text(item.GrammageTitle).FontFamily("Yekan Bakh FaNum");
                            table.Cell().Background(background).Border(1).AlignCenter().Text(item.CountTitle).FontFamily("Yekan Bakh FaNum");
                            table.Cell().Background(background).Border(1).AlignCenter().Text(item.CellophaneTitle).FontFamily("Yekan Bakh FaNum");
                            table.Cell().Background(background).Border(1).AlignCenter().Text(item.UvTitle).FontFamily("Yekan Bakh FaNum");
                            table.Cell().Background(background).Border(1).AlignCenter().Text(item.Price.ToString("N0") ?? "-").FontFamily("Yekan Bakh FaNum");
                        }
                    });
                });
                page.Footer().PaddingHorizontal(0);
            });
        }).GeneratePdf();
        return File(pdfBytes, "application/pdf", $"file.pdf");
    }
}