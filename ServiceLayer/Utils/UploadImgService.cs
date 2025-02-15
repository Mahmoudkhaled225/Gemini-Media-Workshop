using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using ServiceLayer.Abstractions;
using ServiceLayer.Concretes;
using Microsoft.AspNetCore.Http;


namespace ServiceLayer.Utils;

public class UploadImgService : IUploadImgService
{
    private readonly Cloudinary _cloudinary;
    private readonly cloudProvider _options;

    public UploadImgService(IOptions<cloudProvider> options)
    {
        _options = options.Value;
        var account = new Account(
            _options.CloudName,
            _options.ApiKey,
            _options.ApiSecret
        );

        _cloudinary = new Cloudinary(account);
    }
    
    

    public async Task<ImageUploadResult?> UploadImg(IFormFile file, string? folderName)
    {
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            Folder = $"{_options.Folder}/{folderName}/"
        };
        var uploadResult = await _cloudinary.UploadAsync(uploadParams); 
        return uploadResult is { Error: not null } or null ? null : uploadResult;
    }
    
    
    
    public async Task<DeletionResult?> DeleteImg(string imgId)
    {
        var deleteParams = new DeletionParams(imgId);
        var result = await _cloudinary.DestroyAsync(deleteParams);
        return result is { Result: "ok" } ? result : null;
    }
}