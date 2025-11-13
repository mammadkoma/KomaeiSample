namespace KomaeiSample.Client.Pages.Panel;
public partial class EditCategories
{
    private MudForm form = new();
    CategoryVm vm = new();
    CategoryDto[] categories = Array.Empty<CategoryDto>();
    CategoryDto SelectedCategory = new CategoryDto();

    protected override async Task OnInitializedAsync()
    {
        categories = (await http.GetFromJsonAsync<CategoryDto[]>("Category/GetAll"))!;
        vm.Id = CategoriesEnum.Office.ToInt();
        SelectedCategory = categories.FirstOrDefault(x => x.Id == vm.Id)!;
        StateHasChanged();
    }

    private void OnCategoryChange()
    {
        SelectedCategory = categories.FirstOrDefault(x => x.Id == vm.Id)!;
    }

    private async Task Submit()
    {
        await form.Validate();
        if (!form.IsValid) return;

        if (vm.File != null)
        {
            if (vm.File.Size > 300 * 1024) // 300KB
            {
                snackbar.Add("حجم تصویر انتخاب شده بیشتر از 300 کیلوبایت است", Severity.Warning);
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
        var response = await http.PostAsync("Category/Edit", content);
        if (response.IsSuccessStatusCode)
        {
            snackbar.Add("با موفقیت ذخیره شد", Severity.Success);
            vm.File = null;
            categories = (await http.GetFromJsonAsync<CategoryDto[]>("Category/GetAll"))!;
            SelectedCategory = categories.FirstOrDefault(x => x.Id == vm.Id)!;
        }
    }
}