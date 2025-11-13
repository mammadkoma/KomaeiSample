namespace KomaeiSample.Client.Pages.Panel;
public partial class SliderAdd
{
    private SliderVm vm = new();
    [Parameter] public SliderDto? Row { get; set; }
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;
    private MudForm form = new();
    string previewImageUrl = "";

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

            var allowedExtensions = new[] { ".jpg", ".jpeg" };
            var extension = Path.GetExtension(vm.File.Name).ToLower();
            var allowedContentTypes = new[] { "image/png", "image/jpeg" };
            if (!allowedExtensions.Contains(extension) || !allowedContentTypes.Contains(vm.File.ContentType))
            {
                snackbar.Add("فرمت تصویر انتخاب شده غیرمجاز است ، از jpg یا jpeg استفاده کنید", Severity.Warning);
                return;
            }
        }
        using var content = new MultipartFormDataContent();
        if (vm.File is not null)
        {
            var stream = vm.File.OpenReadStream(100 * 1024 * 1024); // 100MB
            content.Add(new StreamContent(stream), "file", vm.File.Name);
        }
        var response = await http.PostAsync("Slider/Add", content);
        if (response.IsSuccessStatusCode)
        {
            snackbar.Add("با موفقیت ذخیره شد", Severity.Success);
            MudDialog.Close(DialogResult.Ok(true));
        }
    }
}