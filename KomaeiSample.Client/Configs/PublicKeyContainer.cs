namespace KomaeiSample.Client.Configs;

public class PublicKeyContainer
{
    public string PublicKeyPem { get; }

    public PublicKeyContainer()
    {
        PublicKeyPem = @"
-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA1B3yVHq/Z+qnLlzz+yEI
+zFhzlBHipVDqSNKgcCcZnXBKcD0ZimNUuGYyzHipfmXgIlm+V5FRoHdsYFJIiSc
eqmbp2x8A4cf4IpmCTwzJ52Ent8WnQrxUDtXQ6OrCh1DJO0+qphGUj7uzrZk8TQC
qZcpeYdsSVWbGhnCz0tHFz2dULPSa8oH3WWBqGq3BEVa07SddOsQMNHOtIXwqbKi
RncTKzqImyr9c/x6OXMZHEoVbpUw7CC3V7043cX21LPuRI12sHsP380Yx5sOOer8
1uNnL59O5Nm27PJlLZr28AtrUGaRjZ4r0aCfNtx0AKXrkNhNCeWRm68mUiMCceEh
4QIDAQAB
-----END PUBLIC KEY-----
";
    }
}
