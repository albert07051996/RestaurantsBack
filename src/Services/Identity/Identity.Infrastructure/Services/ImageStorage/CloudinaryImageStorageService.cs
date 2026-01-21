using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Identity.Application.Interfaces;
using Identity.Infrastructure.Services.ImageStorage;
using Microsoft.Extensions.Options;
using Npgsql.BackendMessages;
using System.Security.Principal;

namespace ProductService.Infrastructure.Services.ImageStorage;

public class CloudinaryImageStorageService : IImageStorageService
{
    private readonly Cloudinary _cloudinary;
    private readonly CloudinarySettings _settings;

    public CloudinaryImageStorageService(IOptions<CloudinarySettings> settings)
    {
        _settings = settings.Value;

        var account = new Account(
            _settings.CloudName,
            _settings.ApiKey,
            _settings.ApiSecret
        );

        _cloudinary = new Cloudinary(account);
    }

    public async Task<string> UploadImageAsync(
        Stream imageStream,
        string fileName,
        CancellationToken cancellationToken = default)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(fileName, imageStream),
            Folder = _settings.DefaultFolder,
            Transformation = new Transformation()
                .Width(1200)
                .Height(800)
                .Crop("limit")
                .Quality("auto:good")
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams, cancellationToken);

        if (uploadResult.Error != null)
        {
            throw new Exception($"Image upload failed: {uploadResult.Error.Message}");
        }

        return uploadResult.PublicId;
    }

    public async Task<bool> DeleteImageAsync(string publicId, CancellationToken cancellationToken = default)
    {
        var deleteParams = new DeletionParams(publicId);
        var result = await _cloudinary.DestroyAsync(deleteParams);

        return result.Result == "ok";
    }

    public Task<string> GetImageUrlAsync(string publicId, int? width = null, int? height = null)
    {
        var transformation = new Transformation();

        if (width.HasValue)
            transformation = transformation.Width(width.Value);

        if (height.HasValue)
            transformation = transformation.Height(height.Value);

        var url = _cloudinary.Api.UrlImgUp.Transform(transformation).BuildUrl(publicId);

        return Task.FromResult(url);
    }
}