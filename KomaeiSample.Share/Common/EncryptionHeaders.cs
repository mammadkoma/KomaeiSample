namespace KomaeiSample.Share.Common;

public static class EncryptionHeaders
{
    public const string Encrypted = "X-Encrypted";
    public const string EncryptedKey = "X-Encrypted-Key";
    public const string EncryptedIv = "X-Encrypted-IV";
    public const string OriginalContentType = "X-Original-ContentType";
}
