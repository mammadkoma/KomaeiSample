namespace KomaeiSample.Server.Services;
public class FileService
{
    public async Task WriteAsync(string guid, IFormFile file, string folder)
    {
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Uploads/{folder}");

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = guid + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
    }

    public void Delete(string fileName, string fileExtension, string folder)
    {
        var directory = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Uploads/{folder}");
        var fileFullName = fileName + fileExtension;
        var fullPath = Path.Combine(directory, fileFullName);
        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }
}