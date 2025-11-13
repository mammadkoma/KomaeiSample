namespace KomaeiSample.Server.Services;
public class EnvelopeService(AppDbContext appDbContext, CategoryService categoryService, SettingService settingService, SliderService sliderService)
{
    public async Task<LayoutDto> GetAll()
    {
        var result = new LayoutDto();
        result.AllCategories = await categoryService.GetAll();
        result.SettingDtos = await settingService.GetAll();
        result.SliderDtos = await sliderService.GetAll();
        result.CardDtos = await GetAllCards();


        // *** copy the same models in EnvelopeHandBag
        //var existRecords = await appDbContext.EnvelopeHandBags.Where(x => x.ModelId == 24).ToListAsync();
        //foreach (var e in existRecords)
        //{
        //    var newRecord = e.Adapt<EnvelopeHandBag>();
        //    newRecord.Id = 0;
        //    newRecord.ModelId = 25;
        //    appDbContext.EnvelopeHandBags.Add(newRecord);
        //}
        //await appDbContext.SaveChangesAsync();

        return result;
    }

    public async Task<List<CardDto>> GetAllCards()
    {
        var result = new List<CardDto>();
        var separator = "<span class='text-black vr mx-1 font14' style='height:19px;vertical-align: middle;'></span>";

        var cardRawOffices = await appDbContext.EnvelopeOffices.AsNoTracking()
            .Where(x => x.Disable == false).ProjectToType<CardRawDto>().ToArrayAsync();
        foreach (var modelId in cardRawOffices.OrderBy(x => x.ModelId).Select(x => x.ModelId).Distinct())
        {
            var papers = cardRawOffices.Where(x => x.ModelId == modelId).DistinctBy(x => x.PaperTitle).OrderBy(x => x.PaperTitle).Select(x => x.PaperTitle).ToList();
            var grammages = cardRawOffices.Where(x => x.ModelId == modelId).OrderBy(x => int.Parse(x.GrammageTitle)).DistinctBy(x => x.GrammageTitle).Select(x => x.GrammageTitle).ToList()!;
            var counts = cardRawOffices.Where(x => x.ModelId == modelId)
                .DistinctBy(x => x.CountTitle).OrderBy(x => int.Parse(x.CountTitle)).Select(x => x.CountTitle).ToList();
            result.Add(new CardDto
            {
                CategoryId = CategoriesEnum.Office.ToInt(),
                CategoryTitle = CategoriesEnum.Office.GetDescription(),
                ModelId = modelId,
                ModelTitle = cardRawOffices.First(x => x.ModelId == modelId).ModelTitle,
                ModelFileName = cardRawOffices.First(x => x.ModelId == modelId).ModelFileName,
                ModelFileExtension = cardRawOffices.First(x => x.ModelId == modelId).ModelFileExtension,
                PapersTitle = string.Join(separator, papers),
                GrammagesTitle = string.Join(separator, grammages),
                CountsTitle = string.Join(separator, counts),
                MinPrice = cardRawOffices.Where(x => x.ModelId == modelId).Min(x => x.Price),
                MaxPrice = cardRawOffices.Where(x => x.ModelId == modelId).Max(x => x.Price),
            });
        }

        var cardRawHospitals = await appDbContext.EnvelopeHospitals.AsNoTracking()
            .Where(x => x.Disable == false).ProjectToType<CardRawDto>().ToArrayAsync();
        foreach (var modelId in cardRawHospitals.OrderBy(x => x.ModelId).Select(x => x.ModelId).Distinct())
        {
            var papers = cardRawHospitals.Where(x => x.ModelId == modelId).DistinctBy(x => x.PaperTitle).OrderBy(x => x.PaperTitle).Select(x => x.PaperTitle).ToList();
            var grammages = cardRawHospitals.Where(x => x.ModelId == modelId).OrderBy(x => int.Parse(x.GrammageTitle)).DistinctBy(x => x.GrammageTitle).Select(x => x.GrammageTitle).ToList()!;
            var counts = cardRawHospitals.Where(x => x.ModelId == modelId)
                .DistinctBy(x => x.CountTitle).OrderBy(x => int.Parse(x.CountTitle)).Select(x => x.CountTitle).ToList();
            result.Add(new CardDto
            {
                CategoryId = CategoriesEnum.Hospital.ToInt(),
                CategoryTitle = CategoriesEnum.Hospital.GetDescription(),
                ModelId = modelId,
                ModelTitle = cardRawHospitals.First(x => x.ModelId == modelId).ModelTitle,
                ModelFileName = cardRawHospitals.First(x => x.ModelId == modelId).ModelFileName,
                ModelFileExtension = cardRawHospitals.First(x => x.ModelId == modelId).ModelFileExtension,
                PapersTitle = string.Join(separator, papers),
                GrammagesTitle = string.Join(separator, grammages),
                CountsTitle = string.Join(separator, counts),
                MinPrice = cardRawHospitals.Where(x => x.ModelId == modelId).Min(x => x.Price),
                MaxPrice = cardRawHospitals.Where(x => x.ModelId == modelId).Max(x => x.Price),
            });
        }

        var cardRawHandBags = await appDbContext.EnvelopeHandBags.AsNoTracking()
            .Where(x => x.Disable == false).ProjectToType<CardRawDto>().ToArrayAsync();
        foreach (var modelId in cardRawHandBags.OrderBy(x => x.ModelId).Select(x => x.ModelId).Distinct())
        {
            var papers = cardRawHandBags.Where(x => x.ModelId == modelId).DistinctBy(x => x.PaperTitle).OrderBy(x => x.PaperTitle).Select(x => x.PaperTitle).ToList();
            var grammages = cardRawHandBags.Where(x => x.ModelId == modelId).OrderBy(x => int.Parse(x.GrammageTitle)).DistinctBy(x => x.GrammageTitle).Select(x => x.GrammageTitle).ToList()!;
            var counts = cardRawHandBags.Where(x => x.ModelId == modelId)
                .DistinctBy(x => x.CountTitle).OrderBy(x => int.Parse(x.CountTitle)).Select(x => x.CountTitle).ToList();
            result.Add(new CardDto
            {
                CategoryId = CategoriesEnum.HandBag.ToInt(),
                CategoryTitle = CategoriesEnum.HandBag.GetDescription(),
                ModelId = modelId,
                ModelTitle = cardRawHandBags.First(x => x.ModelId == modelId).ModelTitle,
                ModelFileName = cardRawHandBags.First(x => x.ModelId == modelId).ModelFileName,
                ModelFileExtension = cardRawHandBags.First(x => x.ModelId == modelId).ModelFileExtension,
                PapersTitle = string.Join(separator, papers),
                GrammagesTitle = string.Join(separator, grammages),
                CountsTitle = string.Join(separator, counts),
                MinPrice = cardRawHandBags.Where(x => x.ModelId == modelId).Min(x => x.Price),
                MaxPrice = cardRawHandBags.Where(x => x.ModelId == modelId).Max(x => x.Price),
            });
        }

        var cardRawConfidentials = await appDbContext.EnvelopeConfidentials.AsNoTracking()
            .Where(x => x.Disable == false).ProjectToType<CardRawDto>().ToArrayAsync();
        foreach (var modelId in cardRawConfidentials.OrderBy(x => x.ModelId).Select(x => x.ModelId).Distinct())
        {
            var papers = cardRawConfidentials.Where(x => x.ModelId == modelId).DistinctBy(x => x.PaperTitle).OrderBy(x => x.PaperTitle).Select(x => x.PaperTitle).ToList();
            var grammages = cardRawConfidentials.Where(x => x.ModelId == modelId).OrderBy(x => int.Parse(x.GrammageTitle)).DistinctBy(x => x.GrammageTitle).Select(x => x.GrammageTitle).ToList()!;
            var counts = cardRawConfidentials.Where(x => x.ModelId == modelId)
                .DistinctBy(x => x.CountTitle).OrderBy(x => int.Parse(x.CountTitle)).Select(x => x.CountTitle).ToList();
            result.Add(new CardDto
            {
                CategoryId = CategoriesEnum.Confidential.ToInt(),
                CategoryTitle = CategoriesEnum.Confidential.GetDescription(),
                ModelId = modelId,
                ModelTitle = cardRawConfidentials.First(x => x.ModelId == modelId).ModelTitle,
                ModelFileName = cardRawConfidentials.First(x => x.ModelId == modelId).ModelFileName,
                ModelFileExtension = cardRawConfidentials.First(x => x.ModelId == modelId).ModelFileExtension,
                PapersTitle = string.Join(separator, papers),
                GrammagesTitle = string.Join(separator, grammages),
                CountsTitle = string.Join(separator, counts),
                MinPrice = cardRawConfidentials.Where(x => x.ModelId == modelId).Min(x => x.Price),
                MaxPrice = cardRawConfidentials.Where(x => x.ModelId == modelId).Max(x => x.Price),
            });
        }

        return result;
    }
}