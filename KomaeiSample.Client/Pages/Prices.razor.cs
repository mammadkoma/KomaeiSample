namespace KomaeiSample.Client.Pages;
public partial class Prices
{
    private async Task OpenPricesOfficeDialog()
    {
        var windowHeight = await jSRuntime.InvokeAsync<int>("getWindowHeight");
        var parameters = new DialogParameters { { "WindowHeight", (windowHeight - 250).ToString() + "px" } };
        var options = ConstantsClient.DialogOptionsLarge;
        var dialog = await dialogService.ShowAsync<PricesOffice>(string.Empty, parameters, options);
    }

    private async Task OpenPricesHospitalDialog()
    {
        var windowHeight = await jSRuntime.InvokeAsync<int>("getWindowHeight");
        var parameters = new DialogParameters { { "WindowHeight", (windowHeight - 250).ToString() + "px" } };
        var options = ConstantsClient.DialogOptionsLarge;
        var dialog = await dialogService.ShowAsync<PricesHospital>(string.Empty, parameters, options);
    }

    private async Task OpenPricesHandBagDialog()
    {
        var windowHeight = await jSRuntime.InvokeAsync<int>("getWindowHeight");
        var parameters = new DialogParameters { { "WindowHeight", (windowHeight - 250).ToString() + "px" } };
        var options = ConstantsClient.DialogOptionsLarge;
        var dialog = await dialogService.ShowAsync<PricesHandBag>(string.Empty, parameters, options);
    }

    private async Task OpenPricesConfidentialDialog()
    {
        var windowHeight = await jSRuntime.InvokeAsync<int>("getWindowHeight");
        var parameters = new DialogParameters { { "WindowHeight", (windowHeight - 250).ToString() + "px" } };
        var options = ConstantsClient.DialogOptionsLarge;
        var dialog = await dialogService.ShowAsync<PricesConfidential>(string.Empty, parameters, options);
    }
}