namespace KomaeiSample.Client.Pages.Panel;

public partial class ProductAdd
{
    private ProductVm vm = new();
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;
    CategoryDto[] categories = Array.Empty<CategoryDto>();

    //protected override async Task OnInitializedAsync()
    //{
    //    categories = (await http.GetFromJsonAsync<CategoryDto[]>("Category/GetAll"))!;
    //}

    //private async Task OnValidSubmit()
    //{
    //    if (vm.File != null)
    //    {
    //        if (vm.File.Size > 300 * 1024)
    //        {
    //            snackbar.Add("حجم تصویر بیشتر از 300 کیلوبایت است", Severity.Warning);
    //            return;
    //        }

    //        var allowedExts = new[] { ".png", ".jpg", ".jpeg" };
    //        var ext = Path.GetExtension(vm.File.Name).ToLower();
    //        if (!allowedExts.Contains(ext))
    //        {
    //            snackbar.Add("فرمت تصویر مجاز نیست (فقط png, jpg, jpeg)", Severity.Warning);
    //            return;
    //        }
    //    }

    //    using var content = new MultipartFormDataContent();
    //    content.Add(new StringContent(vm.CategoryId.ToString()), "CategoryId");
    //    content.Add(new StringContent(vm.Title), "Title");
    //    content.Add(new StringContent(vm.Price.ToString()), "Price");

    //    if (vm.File is not null)
    //    {
    //        var stream = vm.File.OpenReadStream(10 * 1024 * 1024); // تا 10 مگابایت
    //        content.Add(new StreamContent(stream), "file", vm.File.Name);
    //    }

    //    var response = await http.PostAsync("Product/AddOrEdit", content);

    //    if (response.IsSuccessStatusCode)
    //    {
    //        snackbar.Add("محصول با موفقیت ذخیره شد", Severity.Success);
    //        MudDialog.Close(DialogResult.Ok(true));
    //    }
    //    else
    //    {
    //        snackbar.Add("خطا در ذخیره اطلاعات", Severity.Error);
    //    }
    //}




    //private ProductVm vm = new();
    //[Parameter] public ProductDto? Row { get; set; }
    //[CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;
    //private MudForm form = new();
    //string previewImageUrl = "";

    //private async Task OnFilesChanged(InputFileChangeEventArgs e)
    //{
    //    IBrowserFile selectedFile = e.File;

    //    if (selectedFile != null &&
    //        (selectedFile.ContentType == "image/jpeg" || selectedFile.Name.EndsWith(".jpg") || selectedFile.Name.EndsWith(".jpeg")))
    //    {
    //        using var stream = selectedFile.OpenReadStream(maxAllowedSize: 500 * 1024 * 1024); // max 500 MB
    //        using var ms = new MemoryStream();
    //        await stream.CopyToAsync(ms);
    //        var base64 = Convert.ToBase64String(ms.ToArray());
    //        previewImageUrl = $"data:{selectedFile.ContentType};base64,{base64}";
    //    }
    //    else
    //    {
    //        previewImageUrl = null!;
    //        selectedFile = null!;
    //    }
    //}

    //private async Task Submit()
    //{
    //    await form.Validate();
    //    if (!form.IsValid) return;

    //    if (vm.File != null)
    //    {
    //        if (vm.File.Size > 300 * 1024) // 300KB
    //        {
    //            snackbar.Add("حجم تصویر انتخاب شده بیشتر از 300 کیلوبایت است", Severity.Warning);
    //            return;
    //        }

    //        var allowedExtensions = new[] { ".jpg", ".jpeg" };
    //        var extension = Path.GetExtension(vm.File.Name).ToLower();
    //        var allowedContentTypes = new[] { "image/png", "image/jpeg" };
    //        if (!allowedExtensions.Contains(extension) || !allowedContentTypes.Contains(vm.File.ContentType))
    //        {
    //            snackbar.Add("فرمت تصویر انتخاب شده غیرمجاز است ، از jpg یا jpeg استفاده کنید", Severity.Warning);
    //            return;
    //        }
    //    }
    //    using var content = new MultipartFormDataContent();
    //    if (vm.File is not null)
    //    {
    //        var stream = vm.File.OpenReadStream(100 * 1024 * 1024); // 100MB
    //        content.Add(new StreamContent(stream), "file", vm.File.Name);
    //    }
    //    var response = await http.PostAsync("Slider/Add", content);
    //    if (response.IsSuccessStatusCode)
    //    {
    //        snackbar.Add("با موفقیت ذخیره شد", Severity.Success);
    //        MudDialog.Close(DialogResult.Ok(true));
    //    }
    //}
}