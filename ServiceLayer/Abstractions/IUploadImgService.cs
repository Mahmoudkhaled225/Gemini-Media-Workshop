using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace ServiceLayer.Abstractions;

public interface IUploadImgService
{
    Task<ImageUploadResult?> UploadImg(IFormFile file,string? folderName);
    Task<DeletionResult?> DeleteImg(string imgId);
}