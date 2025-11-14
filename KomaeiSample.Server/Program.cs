using KomaeiSample.Server.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddBadRequestHandler(); //
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor(); //
// LocalDbConnectionString - LocalDbConnectionString2 - ServerConnectionString
builder.Services.AddDataBase(builder.Configuration.GetConnectionString("LocalDbConnectionString")!);
builder.Services.RegisterMapsterConfigs(); //

foreach (var appService in typeof(Program).Assembly.GetTypes()
    .Where(s => s.Name.EndsWith("Service") && s.IsInterface == false).ToList())
    builder.Services.Add(new ServiceDescriptor(appService, appService, ServiceLifetime.Scoped));

CultureInfo culture = new CultureInfo("en-US");
culture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
culture.DateTimeFormat.LongTimePattern = "HH:mm:ss";
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

var app = builder.Build();

var privatePem = File.ReadAllText(Path.Combine(app.Environment.ContentRootPath, "private.pem"));
app.UseDecryptRequest(privatePem); //

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
app.AddExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();