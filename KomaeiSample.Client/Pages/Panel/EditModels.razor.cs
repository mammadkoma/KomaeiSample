namespace KomaeiSample.Client.Pages.Panel;
public partial class EditModels
{
    private MudForm form = new();
    ModelVm vm = new();
    CategoryDto[] categories = Array.Empty<CategoryDto>();
    ModelDto[] ModelsAll = Array.Empty<ModelDto>();
    ModelDto[] Models = Array.Empty<ModelDto>();
    CategoryDto SelectedCategory = new CategoryDto();
    ModelDto SelectedModel = new ModelDto();

    protected override async Task OnInitializedAsync()
    {
        categories = (await http.GetFromJsonAsync<CategoryDto[]>("Category/GetAll"))!;
        ModelsAll = (await http.GetFromJsonAsync<ModelDto[]>("Model/GetAll/"))!;
        vm.CategoryId = CategoriesEnum.Office.ToInt();
        SelectedCategory = categories.FirstOrDefault(x => x.Id == vm.Id)!;
        SelectedModel = ModelsAll.FirstOrDefault(x => x.Id == 1)!;
        OnCategoryChange();
        StateHasChanged();
    }

    private void OnCategoryChange()
    {
        SelectedCategory = categories.FirstOrDefault(x => x.Id == vm.CategoryId)!;
        Models = ModelsAll.Where(x => x.CategoryId == vm.CategoryId).ToArray();
        vm.Id = null;
        SelectedModel = new ModelDto();
    }

    private void OnModelChange()
    {
        SelectedModel = ModelsAll.FirstOrDefault(x => x.Id == vm.Id)!;
    }

    private async Task Submit()
    {
        await form.Validate();
        if (!form.IsValid) return;

        if (vm.File != null)
        {
            if (vm.File.Size > 200 * 1024) // 200KB
            {
                snackbar.Add("حجم تصویر انتخاب شده بیشتر از 200 کیلوبایت است", Severity.Warning);
                return;
            }

            var allowedExtensions = new[] { ".png", ".jpg", ".jpeg" };
            var extension = Path.GetExtension(vm.File.Name).ToLower();
            var allowedContentTypes = new[] { "image/png", "image/jpeg" };
            if (!allowedExtensions.Contains(extension) || !allowedContentTypes.Contains(vm.File.ContentType))
            {
                snackbar.Add("فرمت تصویر انتخاب شده غیرمجاز است ، از png ، jpg یا jpeg استفاده کنید", Severity.Warning);
                return;
            }
        }
        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(vm.Id.ToString()!), "Id");
        if (vm.File is not null)
        {
            var stream = vm.File.OpenReadStream(100 * 1024 * 1024); // 100MB
            content.Add(new StreamContent(stream), "file", vm.File.Name);
        }
        var response = await http.PostAsync("Model/Edit", content);
        if (response.IsSuccessStatusCode)
        {
            snackbar.Add("با موفقیت ذخیره شد", Severity.Success);
            vm.File = null;
            ModelsAll = (await http.GetFromJsonAsync<ModelDto[]>("Model/GetAll/"))!;
            SelectedModel = ModelsAll.FirstOrDefault(x => x.Id == vm.Id)!;
        }
    }
}