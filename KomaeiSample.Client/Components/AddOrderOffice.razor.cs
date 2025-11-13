using KomaeiSample.Client.Pages;

namespace KomaeiSample.Client.Components;
public partial class AddOrderOffice
{
    [Parameter] public EnvelopeOfficeDto[] EnvelopeOffices { get; set; } = Array.Empty<EnvelopeOfficeDto>();
    [Parameter] public int CategoryId { get; set; }
    [Parameter] public int ModelId { get; set; }
    OrderVm vm = new();
    ModelDto[]? Models = Array.Empty<ModelDto>();
    PaperDto[]? Papers = Array.Empty<PaperDto>();
    GrammageDto[]? Grammages = Array.Empty<GrammageDto>();
    CountDto[]? Counts = Array.Empty<CountDto>();
    HasOrNotDto[]? HasInternalTerams = Array.Empty<HasOrNotDto>();
    HasOrNotDto[]? HasDoorGlues = Array.Empty<HasOrNotDto>();
    private MudForm form = new();
    string previewImageUrl = "";
    decimal TotalPrice = 0;
    DiscountDto? disCountDto = null;

    protected override Task OnInitializedAsync()
    {
        vm.CategoryId = CategoryId;
        Models = EnvelopeOffices.Where(x => x.ModelId == ModelId).Select(x => new ModelDto { Id = x.ModelId, Title = x.ModelTitle, FileName = x.ModelFileName, FileExtension = x.ModelFileExtension }).DistinctBy(x => x.Id).ToArray();
        vm.ModelId = ModelId;
        OnModelChange();
        StateHasChanged();
        return base.OnInitializedAsync();
    }

    private void OnModelChange()
    {
        vm.PaperId = null; Papers = null;
        vm.GrammageId = null; Grammages = null;
        vm.CountId = null; Counts = null;
        vm.HasInternalTeramId = null; HasInternalTerams = null;
        vm.HasDoorGlueId = null; HasDoorGlues = null;
        vm.Price = null; vm.EnvelopeId = null;
        Papers = EnvelopeOffices.Where(x => x.ModelId == vm.ModelId).Select(x => new PaperDto { Id = x.PaperId, Title = x.PaperTitle }).OrderBy(x => x.Title).DistinctBy(x => x.Id).ToArray();
    }

    private void OnPaperChange()
    {
        vm.GrammageId = null; Grammages = null;
        vm.CountId = null; Counts = null;
        vm.HasInternalTeramId = null; HasInternalTerams = null;
        vm.HasDoorGlueId = null; HasDoorGlues = null;
        vm.Price = null; vm.EnvelopeId = null;
        Grammages = EnvelopeOffices.Where(x => x.ModelId == vm.ModelId && x.PaperId == vm.PaperId)
            .Select(x => new GrammageDto { Id = x.GrammageId, Title = x.GrammageTitle }).OrderBy(x => int.Parse(x.Title)).DistinctBy(x => x.Id).ToArray();
    }

    private void OnGrammageChange()
    {
        vm.CountId = null; Counts = null;
        vm.HasInternalTeramId = null; HasInternalTerams = null;
        vm.HasDoorGlueId = null; HasDoorGlues = null;
        vm.Price = null; vm.EnvelopeId = null;
        Counts = EnvelopeOffices.Where(x => x.ModelId == vm.ModelId && x.PaperId == vm.PaperId && x.GrammageId == vm.GrammageId).OrderBy(x => int.Parse(x.CountTitle)).Select(x => new CountDto { Id = x.CountId, Title = x.CountTitle }).DistinctBy(x => x.Id).ToArray();
    }

    private void OnCountChange()
    {
        vm.HasInternalTeramId = null; HasInternalTerams = null;
        vm.HasDoorGlueId = null; HasDoorGlues = null;
        vm.Price = null; vm.EnvelopeId = null;
        HasInternalTerams = EnvelopeOffices.Where(x => x.ModelId == vm.ModelId && x.PaperId == vm.PaperId && x.GrammageId == vm.GrammageId && x.CountId == vm.CountId).Select(x => new HasOrNotDto { Id = x.HasInternalTeram, Title = x.HasInternalTeram.ToHasOrNotTitle() }).OrderBy(x => x.Title).DistinctBy(x => x.Id).ToArray();
    }

    private void OnHasInternalTeramChange()
    {
        vm.HasDoorGlueId = null; HasDoorGlues = null;
        vm.Price = null; vm.EnvelopeId = null;
        HasDoorGlues = EnvelopeOffices.Where(x => x.ModelId == vm.ModelId && x.PaperId == vm.PaperId && x.GrammageId == vm.GrammageId && x.CountId == vm.CountId && x.HasInternalTeram == vm.HasInternalTeramId).Select(x => new HasOrNotDto { Id = x.HasDoorGlue, Title = x.HasDoorGlue.ToHasOrNotTitle() }).OrderBy(x => x.Title).DistinctBy(x => x.Id).ToArray();
    }

    private void OnHasDoorGlueChange()
    {
        try
        {
            var envelopeOffice = EnvelopeOffices.Where(x => x.ModelId == vm.ModelId && x.PaperId == vm.PaperId && x.GrammageId == vm.GrammageId && x.CountId == vm.CountId && x.HasInternalTeram == vm.HasInternalTeramId && x.HasDoorGlue == vm.HasDoorGlueId).First();
            vm.EnvelopeId = envelopeOffice.Id;
            vm.Price = envelopeOffice.Price;
            TotalPrice = vm.Price.Value * vm.Series!.Value;
        }
        catch
        {
            snackbar.Add("قیمت ثبت نشده است", Severity.Error);
            vm.EnvelopeId = null;
            vm.Price = null;
        }
        finally
        {
            if (vm.EnvelopeId == null || vm.Price == null)
            {
                snackbar.Add("قیمت ثبت نشده است", Severity.Error);
                vm.EnvelopeId = null;
                vm.Price = null;
            }
        }
    }

    private async Task Submit()
    {
        await form.Validate();
        if (!form.IsValid)
            return;

        if (vm.File != null)
        {
            var acceptedFormat = new List<string> { ".jpg", ".jpeg" };
            var fileName = vm.File!.Name.ToLower();
            var fileExtension = Path.GetExtension(fileName);
            if (acceptedFormat.Contains(fileExtension) == false)
            {
                snackbar.Add("فقط فایل با فرمت jpg یا jpeg قابل قبول است", Severity.Warning);
                return;
            }
        }

        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(vm.CategoryId.ToString()), "CategoryId");
        content.Add(new StringContent(vm.EnvelopeId?.ToString() ?? ""), "EnvelopeId");
        content.Add(new StringContent(vm.Series?.ToString() ?? ""), "Series");
        content.Add(new StringContent(vm.Price?.ToString() ?? ""), "Price");
        if (disCountDto != null)
        {
            content.Add(new StringContent(disCountDto.Code), "DiscountCode");
            content.Add(new StringContent(disCountDto.Id.ToString()), "DiscountId");
        }
        content.Add(new StringContent(vm.DeliveryMethodId?.ToString() ?? ""), "DeliveryMethodId");
        if (vm.AddressTypeId != null)
            content.Add(new StringContent(vm.AddressTypeId?.ToString() ?? ""), "AddressTypeId");
        if (vm.TehranAreaId != null)
            content.Add(new StringContent(vm.TehranAreaId?.ToString() ?? ""), "TehranAreaId");
        if (vm.Address!.IsNullOrEmpty() != true)
            content.Add(new StringContent(vm.Address!), "Address");
        if (vm.PostalCode!.IsNullOrEmpty() != true)
            content.Add(new StringContent(vm.PostalCode!), "PostalCode");
        if (vm.CellophaneId != null)
            content.Add(new StringContent(vm.CellophaneId.ToString() ?? ""), "CellophaneId");
        if (vm.UvId != null)
            content.Add(new StringContent(vm.UvId.ToString() ?? ""), "UvId");
        if (vm.Desc!.IsNullOrEmpty() != true)
            content.Add(new StringContent(vm.Desc!), "Desc");

        if (vm.File is not null)
        {
            var stream = vm.File.OpenReadStream(500 * 1024 * 1024); // max 500MB
            content.Add(new StreamContent(stream), "file", vm.File.Name);
        }

        var response = await http.PostAsync("Order/Add", content);
        if (response.IsSuccessStatusCode)
        {
            navigationManager.NavigateTo("OrderAddedSuccessfully");
        }
    }

    private async Task OnFilesChanged(InputFileChangeEventArgs e)
    {
        IBrowserFile selectedFile = e.File;

        if (selectedFile != null &&
            (selectedFile.ContentType == "image/jpeg" || selectedFile.Name.EndsWith(".jpg") || selectedFile.Name.EndsWith(".jpeg")))
        {
            using var stream = selectedFile.OpenReadStream(maxAllowedSize: 500 * 1024 * 1024); // max 500 MB
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            var base64 = Convert.ToBase64String(ms.ToArray());
            previewImageUrl = $"data:{selectedFile.ContentType};base64,{base64}";
        }
        else
        {
            previewImageUrl = null!;
            selectedFile = null!;
        }
    }

    private async Task OpenUserAddressesDialog()
    {
        var parameters = new DialogParameters { { nameof(UserAddresses.SelectableMode), true } };
        var options = ConstantsClient.DialogOptionsLarge;
        var dialog = await dialogService.ShowAsync<UserAddresses>("آدرس های من", parameters, options);
        var dialogResult = await dialog.Result;
        if (dialogResult!.Canceled == false && dialogResult.Data is string selectedAddress)
            vm.Address = selectedAddress;
    }

    private long? GetTotalCount()
    {
        if (vm.CountId != null && vm.Series != null)
        {
            var count = Counts!.First(x => x.Id == vm.CountId).Title;
            return int.Parse(count) * vm.Series;
        }
        return null;
    }

    private decimal? GetTotalPrice()
    {
        if (vm.Price != null && vm.Price != null)
        {
            return vm.Price * vm.Series;
        }
        return null;
    }

    private async Task GetDiscountByCodeAndCategoryId()
    {
        if (vm.DiscountCode!.IsNullOrEmpty())
        {
            snackbar.Add("کد تخفیف را وارد کنید", Severity.Warning);
            return;
        }
        disCountDto = null;
        var response = await http.PostAsJsonAsync($"Discount/GetByCodeAndCategoryId?code={vm.DiscountCode}&categoryId={vm.CategoryId}", new GetDiscountByCodeAndCategoryIdVm { Code = vm.DiscountCode!, CategoryId = vm.CategoryId });
        if (response.IsSuccessStatusCode)
        {
            disCountDto = await response.Content.ReadFromJsonAsync<DiscountDto>();
        }
    }
}