namespace KomaeiSample.Client.Pages;

public partial class ProductAddEdit
{
    private ProductVm vm = new();
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;
    CategoryDto[] CategoryDtos = Array.Empty<CategoryDto>();

    protected override async Task OnInitializedAsync()
    {
        CategoryDtos = (await http.GetFromJsonAsync<CategoryDto[]>("Category/GetAll"))!;
    }

    private async Task OnValidSubmit()
    {
        if (vm.File == null)
        {
            snackbar.Add("تصویر محصول را انتخاب کنید", Severity.Warning);
            return;
        }
        if (vm.File != null)
        {
            if (vm.File.Size > 300 * 1024)
            {
                snackbar.Add("حجم تصویر بیشتر از 300 کیلوبایت است", Severity.Warning);
                return;
            }

            var allowedExts = new[] { ".png", ".jpg", ".jpeg" };
            var ext = Path.GetExtension(vm.File.Name).ToLower();
            if (!allowedExts.Contains(ext))
            {
                snackbar.Add("فرمت تصویر مجاز نیست (فقط png, jpg, jpeg)", Severity.Warning);
                return;
            }
        }

        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(vm.CategoryId.ToString()!), "CategoryId");
        content.Add(new StringContent(vm.Title), "Title");
        content.Add(new StringContent(vm.Price.ToString()!), "Price");

        if (vm.File is not null)
        {
            var fileStream = vm.File.OpenReadStream(10_000_000);
            var fileContent = new StreamContent(fileStream);
            content.Add(fileContent, nameof(vm.FileForServer), vm.File.Name);
        }

        var response = await http.PostAsync("Product/AddEdit", content);

        if (response.IsSuccessStatusCode)
        {
            snackbar.Add("محصول با موفقیت ذخیره شد", Severity.Success);
            MudDialog.Close(DialogResult.Ok(true));
        }
        else
            snackbar.Add("خطا در ذخیره اطلاعات", Severity.Error);
    }
}