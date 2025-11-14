var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// MudBlazor
builder.Services.AddMudServices();
builder.Services.AddSingleton<ISnackbar, SnackbarService>();
builder.Services.AddTransient<MudLocalizer, DictionaryMudLocalizer>();

// API services
var services = typeof(Program).Assembly.GetTypes()
    .Where(x => x.Name.EndsWith("Service") && !x.IsInterface);

foreach (var service in services)
    builder.Services.AddSingleton(service);

// Auth
builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton<AuthenticationStateProvider, AppAuthStateProvider>();

// Public key stored in a .cs file
builder.Services.AddSingleton<PublicKeyContainer>();

builder.Services.AddTransient<EncryptionHandler>();

builder.Services.AddHttpClient("http", c =>
{
    c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress + "api/");
})
.AddHttpMessageHandler<EncryptionHandler>()
.AddHttpMessageHandler<HttpStatusCodeService>();

builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("http"));

CultureInfo culture = new CultureInfo("en-US");
culture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
culture.DateTimeFormat.LongTimePattern = "HH:mm:ss";

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CreateSpecificCulture("fa-IR");

await builder.Build().RunAsync();