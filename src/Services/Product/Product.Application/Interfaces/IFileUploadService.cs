namespace Product.Application.Interfaces;

public interface IFileUploadService
{
    Task<string> UploadImageAsync(Stream fileStream, string fileName, string folderPath = "menu-items");
    Task<string> UploadVideoAsync(Stream fileStream, string fileName, string folderPath = "videos");
    Task<bool> DeleteFileAsync(string fileUrl);
    Task<List<string>> UploadMultipleImagesAsync(List<(Stream stream, string fileName)> files, string folderPath = "menu-items");
}

public class UploadFileResult
{
    public bool Success { get; set; }
    public string? FileUrl { get; set; }
    public string? ErrorMessage { get; set; }
}
