namespace KomaeiSample.Server.Controllers;
public class ShareController(ModelService modelService, PaperService paperService, GrammageService grammageService, CountService countService, HospitalTemplateService hospitalTemplateService, CellophaneService cellophaneService, UvService uvService) : AppController
{
    [HttpGet("{categoryId}")]
    [Authorize(Roles = $"{Constants.AdminRole},{Constants.SuperAdminRole}")]
    public async Task<ActionResult> GetAllModelPaperGrammageCountByCategoryId(int categoryId)
    {

        return Ok(new ModelPaperGrammageCountDto
        {
            Models = await modelService.GetAllByCategoryId(categoryId),
            Papers = await paperService.GetAllByCategoryId(categoryId),
            Grammages = await grammageService.GetAllByCategoryId(categoryId),
            Counts = await countService.GetAllByCategoryId(categoryId),
        });
    }

    [HttpGet("{categoryId}")]
    [Authorize(Roles = $"{Constants.AdminRole},{Constants.SuperAdminRole}")]
    public async Task<ActionResult> GetAddEditEnvelopeHospitalSelectDataByCategoryId(int categoryId)
    {

        return Ok(new EnvelopeHospitalAddEditPageDto
        {
            Models = await modelService.GetAllByCategoryId(categoryId),
            HospitalTemplates = await hospitalTemplateService.GetAll(),
            Papers = await paperService.GetAllByCategoryId(categoryId),
            Grammages = await grammageService.GetAllByCategoryId(categoryId),
            Counts = await countService.GetAllByCategoryId(categoryId),
            Cellophanes = await cellophaneService.GetAll(),
            Uvs = await uvService.GetAll(),
        });
    }

    [HttpGet("{categoryId}")]
    [Authorize(Roles = $"{Constants.AdminRole},{Constants.SuperAdminRole}")]
    public async Task<ActionResult> GetAllModelPaperGrammageCountCellophaneUvDtoByCategoryId(int categoryId)
    {

        return Ok(new ModelPaperGrammageCountCellophaneUvDto
        {
            Models = await modelService.GetAllByCategoryId(categoryId),
            Papers = await paperService.GetAllByCategoryId(categoryId),
            Grammages = await grammageService.GetAllByCategoryId(categoryId),
            Counts = await countService.GetAllByCategoryId(categoryId),
            Cellophanes = await cellophaneService.GetAll(),
            Uvs = await uvService.GetAll(),
        });
    }
}
