using System.Net.Http.Headers;

namespace KomaeiSample.Client.Configs;

public class EncryptionHandler : DelegatingHandler
{
    private readonly IJSRuntime _js;
    private readonly PublicKeyContainer _container;

    public EncryptionHandler(IJSRuntime js, PublicKeyContainer container)
    {
        _js = js;
        _container = container;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.Content != null)
        {
            // read original content bytes (works for JSON and multipart/form-data)
            var originalBytes = await request.Content.ReadAsByteArrayAsync(cancellationToken);
            var contentType = request.Content.Headers.ContentType?.ToString() ?? "application/octet-stream";

            // convert to base64 string for safe JS interop
            var base64Body = Convert.ToBase64String(originalBytes);

            // call JS which will return base64 strings for cipher, key, iv
            var jsResult = await _js.InvokeAsync<JsEncryptedResult>(
                "encryptRequest",
                _container.PublicKeyPem,
                base64Body,
                contentType
            );

            // set headers and new binary content (decoded from base64)
            request.Headers.Remove(EncryptionHeaders.Encrypted);
            request.Headers.Add(EncryptionHeaders.Encrypted, "hybrid");
            request.Headers.Remove(EncryptionHeaders.EncryptedKey);
            request.Headers.Add(EncryptionHeaders.EncryptedKey, jsResult.encryptedKey);
            request.Headers.Remove(EncryptionHeaders.EncryptedIv);
            request.Headers.Add(EncryptionHeaders.EncryptedIv, jsResult.iv);
            request.Headers.Remove(EncryptionHeaders.OriginalContentType);
            request.Headers.Add(EncryptionHeaders.OriginalContentType, contentType);

            var cipherBytes = Convert.FromBase64String(jsResult.cipher);
            var newContent = new ByteArrayContent(cipherBytes);
            newContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            request.Content = newContent;
        }

        return await base.SendAsync(request, cancellationToken);
    }

    // shape must match JS return (all base64 strings)
    private class JsEncryptedResult
    {
        public string cipher { get; set; } = "";
        public string encryptedKey { get; set; } = "";
        public string iv { get; set; } = "";
        public string contentType { get; set; } = "";
    }
}
