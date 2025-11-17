namespace KomaeiSample.Client.Configs;

public static class BrowserFileExtension
{
    public static ValidateDto Validate(this IBrowserFile file, int maxLengthKb)
    {
        if (file != null)
        {
            if (file.Size > maxLengthKb * 1024)
                return new ValidateDto { IsValid = false, Message = $"حجم تصویر بیشتر از {maxLengthKb} کیلوبایت است" };

            var allowedExts = new[] { ".png", ".jpg", ".jpeg" };
            var ext = Path.GetExtension(file.Name).ToLower();
            if (!allowedExts.Contains(ext))
                return new ValidateDto { IsValid = false, Message = "فرمت تصویر مجاز نیست (فقط png, jpg, jpeg)" };
        }

        return new ValidateDto { IsValid = true, Message = "" };
    }
}
