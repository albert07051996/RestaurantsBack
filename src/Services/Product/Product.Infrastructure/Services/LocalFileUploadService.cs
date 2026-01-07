using Product.Application.Interfaces;

namespace Product.Infrastructure.Services;

public class LocalFileUploadService : IFileUploadService
{
    private readonly string _uploadPath;
    private readonly string _baseUrl;

    public LocalFileUploadService(string uploadPath = "uploads", string baseUrl = "https://localhost:5002")
    {
        _uploadPath = uploadPath;
        _baseUrl = baseUrl;
        
        // Create upload directories if they don't exist
        Directory.CreateDirectory(Path.Combine(_uploadPath, "menu-items"));
        Directory.CreateDirectory(Path.Combine(_uploadPath, "videos"));
        Directory.CreateDirectory(Path.Combine(_uploadPath, "backgrounds"));
        Directory.CreateDirectory(Path.Combine(_uploadPath, "logos"));
    }

    public async Task<string> UploadImageAsync(Stream fileStream, string fileName, string folderPath = "menu-items")
    {
        try
        {
            var fileExtension = Path.GetExtension(fileName);
            var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            var fullPath = Path.Combine(_uploadPath, folderPath, uniqueFileName);

            using (var fileStreamOutput = new FileStream(fullPath, FileMode.Create))
            {
                await fileStream.CopyToAsync(fileStreamOutput);
            }

            return $"{_baseUrl}/uploads/{folderPath}/{uniqueFileName}";
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to upload image: {ex.Message}");
        }
    }

    public async Task<string> UploadVideoAsync(Stream fileStream, string fileName, string folderPath = "videos")
    {
        try
        {
            var fileExtension = Path.GetExtension(fileName);
            var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            var fullPath = Path.Combine(_uploadPath, folderPath, uniqueFileName);

            using (var fileStreamOutput = new FileStream(fullPath, FileMode.Create))
            {
                await fileStream.CopyToAsync(fileStreamOutput);
            }

            return $"{_baseUrl}/uploads/{folderPath}/{uniqueFileName}";
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to upload video: {ex.Message}");
        }
    }

    public async Task<bool> DeleteFileAsync(string fileUrl)
    {
        try
        {
            if (string.IsNullOrEmpty(fileUrl))
                return false;

            var fileName = fileUrl.Replace($"{_baseUrl}/uploads/", "");
            var fullPath = Path.Combine(_uploadPath, fileName);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<string>> UploadMultipleImagesAsync(List<(Stream stream, string fileName)> files, string folderPath = "menu-items")
    {
        var uploadedUrls = new List<string>();

        foreach (var file in files)
        {
            try
            {
                var url = await UploadImageAsync(file.stream, file.fileName, folderPath);
                uploadedUrls.Add(url);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to upload file {file.fileName}: {ex.Message}");
            }
        }

        return uploadedUrls;
    }
}
