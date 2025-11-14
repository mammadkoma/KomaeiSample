namespace KomaeiSample.Server.Middleware;

public class DecryptRequestMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _privateKeyPem;

    public DecryptRequestMiddleware(RequestDelegate next, string privateKeyPem)
    {
        _next = next;
        _privateKeyPem = privateKeyPem;
    }

    public async Task Invoke(HttpContext context)
    {
        var req = context.Request;

        if (req.Headers.ContainsKey(EncryptionHeaders.Encrypted) &&
            req.Headers[EncryptionHeaders.Encrypted].ToString() == "hybrid")
        {
            // read encrypted body
            using var ms = new MemoryStream();
            await req.Body.CopyToAsync(ms);
            var cipherBytes = ms.ToArray();

            var encryptedKeyBase64 = req.Headers[EncryptionHeaders.EncryptedKey].ToString();
            var ivBase64 = req.Headers[EncryptionHeaders.EncryptedIv].ToString();

            if (string.IsNullOrEmpty(encryptedKeyBase64) || string.IsNullOrEmpty(ivBase64))
                throw new InvalidOperationException("Missing encryption headers.");

            var encryptedKey = Convert.FromBase64String(encryptedKeyBase64);
            var iv = Convert.FromBase64String(ivBase64);

            // Decrypt AES key with RSA private key (OAEP SHA-256)
            byte[] aesKey;
            using (var rsa = RSA.Create())
            {
                rsa.ImportFromPem(_privateKeyPem.ToCharArray());
                aesKey = rsa.Decrypt(encryptedKey, RSAEncryptionPadding.OaepSHA256);
            }

            // WebCrypto AES-GCM ciphertext includes tag appended at the end (tag length 16)
            if (cipherBytes.Length < 16) throw new InvalidOperationException("Ciphertext too short.");
            var tag = cipherBytes[^16..];
            var ctOnly = cipherBytes[..^16];

            var plain = new byte[ctOnly.Length];
            try
            {
                using var aes = new AesGcm(aesKey);
                aes.Decrypt(iv, ctOnly, tag, plain);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to decrypt request body.", ex);
            }

            // restore original content-type
            if (req.Headers.ContainsKey(EncryptionHeaders.OriginalContentType))
                req.ContentType = req.Headers[EncryptionHeaders.OriginalContentType].ToString();
            else
                req.ContentType = "application/octet-stream";

            req.Body = new MemoryStream(plain);
            req.Body.Seek(0, SeekOrigin.Begin);
        }

        await _next(context);
    }
}

// extension (to register in Program.cs)
public static class DecryptRequestMiddlewareExtensions
{
    public static IApplicationBuilder UseDecryptRequest(this IApplicationBuilder app, string privateKeyPem)
        => app.UseMiddleware<DecryptRequestMiddleware>(privateKeyPem);
}
