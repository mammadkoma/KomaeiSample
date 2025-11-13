namespace KomaeiSample.Server.Services;
public class SmsService
{
    public async Task Send(string mobile, string text)
    {
        var apiUrl = "https://api.mediana.ir/sms/v1/send/sms";
        var apiKey = "8FDLXquDY5Oren9o+07fSYxTAddat7gdu1GVRwKOiw0=";
        var httpClient = new HttpClient();
        var payload = new
        {
            type = "Informational",
            recipients = new[] { mobile },
            messageText = text,
            dateTime = "2024-12-31T07:57:54.889Z"
        };
        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
        var response = await httpClient.PostAsync(apiUrl, content);
        //var responseContent = await response.Content.ReadAsStringAsync();
        //Console.WriteLine($"Status Code: {response.StatusCode}");
        //Console.WriteLine("Response:");
        //Console.WriteLine(responseContent);
    }

    public async Task SendOtp(string mobile, string confirmCode)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.mediana.ir/sms/v1/send/pattern");
        request.Headers.Add("accept", "*/*");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "8FDLXquDY5Oren9o+07fSYxTAddat7gdu1GVRwKOiw0=");
        var payload = new
        {
            recipients = new[] { mobile },
            patternCode = "802591",
            parameters = new
            {
                otp = confirmCode
            }
        };
        var json = JsonSerializer.Serialize(payload);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.SendAsync(request);
        //response.EnsureSuccessStatusCode();
        //Console.WriteLine(await response.Content.ReadAsStringAsync());
    }

    public async Task SendChangeOrderStatus(string mobile, string orderId, string orderStatusTitle)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.mediana.ir/sms/v1/send/pattern");
        request.Headers.Add("accept", "*/*");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "8FDLXquDY5Oren9o+07fSYxTAddat7gdu1GVRwKOiw0=");
        var payload = new
        {
            recipients = new[] { mobile },
            patternCode = "803104", // 
            parameters = new
            {
                token1 = orderId,
                token2 = orderStatusTitle
            }
        };
        var json = JsonSerializer.Serialize(payload);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.SendAsync(request);
        //response.EnsureSuccessStatusCode();
        //var aaa = await response.Content.ReadAsStringAsync();
        //Console.WriteLine(await response.Content.ReadAsStringAsync());
    }

    public async Task SendChangeOrderStatus3(string mobile, string orderId)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.mediana.ir/sms/v1/send/pattern");
        request.Headers.Add("accept", "*/*");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "8FDLXquDY5Oren9o+07fSYxTAddat7gdu1GVRwKOiw0=");
        var payload = new
        {
            recipients = new[] { mobile },
            patternCode = "803884", // 
            parameters = new
            {
                token1 = orderId,
            }
        };
        var json = JsonSerializer.Serialize(payload);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.SendAsync(request);
        //response.EnsureSuccessStatusCode();
        //var aaa = await response.Content.ReadAsStringAsync();
        //Console.WriteLine(await response.Content.ReadAsStringAsync());
    }

    public async Task SendChangeOrderStatus4(string mobile, string orderId)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.mediana.ir/sms/v1/send/pattern");
        request.Headers.Add("accept", "*/*");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "8FDLXquDY5Oren9o+07fSYxTAddat7gdu1GVRwKOiw0=");
        var payload = new
        {
            recipients = new[] { mobile },
            patternCode = "803885", // 
            parameters = new
            {
                token1 = orderId,
            }
        };
        var json = JsonSerializer.Serialize(payload);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.SendAsync(request);
        //response.EnsureSuccessStatusCode();
        //var aaa = await response.Content.ReadAsStringAsync();
        //Console.WriteLine(await response.Content.ReadAsStringAsync());
    }
}