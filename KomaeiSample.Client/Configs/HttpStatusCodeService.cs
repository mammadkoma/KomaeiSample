namespace KomaeiSample.Client.Configs;
public class HttpStatusCodeService(ISnackbar snackbar, LocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider, ApiService apiService) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // before sending the request
        apiService.IncreaseApiCount();
        string[] notNeedTokenPages = ["login", "register", "order", "contactus"];
        if (notNeedTokenPages.Contains(request.RequestUri!.AbsolutePath.ToLower()))
            return await base.SendAsync(request, cancellationToken);

        var token = await localStorageService.GetItem<string>("token");
        if (string.IsNullOrEmpty(token) == false)
            request.Headers.Add("Authorization", $"Bearer {token}");

        var response = await base.SendAsync(request, cancellationToken);


        // after sending the request
        if (response.IsSuccessStatusCode == false)
        {
            if (response.StatusCode == HttpStatusCode.Conflict) // 409
                snackbar.Add(await response.Content.ReadAsStringAsync(), Severity.Warning);

            if (response.StatusCode == HttpStatusCode.BadRequest) // 400
            {
                string json = await response.Content.ReadAsStringAsync();
                using JsonDocument doc = JsonDocument.Parse(json);
                JsonElement root = doc.RootElement;
                if (root.TryGetProperty("message", out JsonElement messageElement))
                {
                    string message = messageElement.GetString()!;
                    snackbar.Add(message, Severity.Warning);
                }
            }

            if (response.StatusCode == HttpStatusCode.InternalServerError) // 500
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    var schema = "";
                    var table = "";
                    var match = Regex.Match(errorMessage, @"table ""(?<schema>\w+)\.(?<table>\w+)""");
                    if (match.Success)
                    {
                        schema = match.Groups["schema"].Value;
                        table = match.Groups["table"].Value;
                        //Console.WriteLine($"Schema: {schema}, Table: {table}");
                    }
                    snackbar.Add($"This record is used in table : \"{schema}.{table}\"", Severity.Warning);
                }
                else
                    snackbar.Add($"Server error occurred : {errorMessage}", Severity.Error);
            }

            if (response.StatusCode == HttpStatusCode.Forbidden) // 403
                snackbar.Add("You have not access permission", Severity.Warning);

            if (response.StatusCode == HttpStatusCode.Unauthorized) // 401
                await (authenticationStateProvider as AppAuthStateProvider)!.Logout(forceLoad: true);
        }
        apiService.DecreaseApiCount();
        return response;
    }
}