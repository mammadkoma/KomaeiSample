namespace KomaeiSample.Client.Pages;

public partial class ProductAddEdit
{
    private ProductVmClient vm = new();
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;
    CategoryDto[] CategoryDtos = Array.Empty<CategoryDto>();

    protected override async Task OnInitializedAsync()
    {
        CategoryDtos = (await http.GetFromJsonAsync<CategoryDto[]>("Category/GetAll"))!;
    }

    private async Task OnValidSubmit()
    {
        var fileForClientValidateResult = vm.FileForClient.Validate(300);
        if (fileForClientValidateResult.IsValid == false)
        {
            snackbar.Add(fileForClientValidateResult.Message, Severity.Warning);
            return;
        }

        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(vm.CategoryId.ToString()!), "CategoryId");
        content.Add(new StringContent(vm.Title), "Title");
        content.Add(new StringContent(vm.Price.ToString()!), "Price");

        if (vm.FileForClient is not null)
        {
            var fileStream = vm.FileForClient.OpenReadStream(10_000_000);
            var fileContent = new StreamContent(fileStream);
            content.Add(fileContent, "FileForServer", vm.FileForClient.Name);
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