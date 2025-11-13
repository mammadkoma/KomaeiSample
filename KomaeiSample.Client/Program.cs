var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// MudBlazor
builder.Services.AddMudServices();
builder.Services.AddSingleton<ISnackbar, SnackbarService>();
builder.Services.AddTransient<MudLocalizer, DictionaryMudLocalizer>();

// Singleton AppServices
var appServices = typeof(Program).Assembly.GetTypes()
    .Where(s => s.Name.EndsWith("Service") && s.IsInterface == false).ToList();
foreach (var appService in appServices)
    builder.Services.Add(new ServiceDescriptor(appService, appService, ServiceLifetime.Singleton));

// AuthStateProvider
builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton<AuthenticationStateProvider, AppAuthStateProvider>();

// HttpClient
builder.Services.AddHttpClient("http", client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress + "api/");
}).AddHttpMessageHandler<HttpStatusCodeService>();
builder.Services.AddSingleton(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("http"));

// CultureInfo for DateTime
CultureInfo culture = new CultureInfo("en-US");
culture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
culture.DateTimeFormat.LongTimePattern = "HH:mm:ss";
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CreateSpecificCulture("fa-IR");

await builder.Build().RunAsync();
