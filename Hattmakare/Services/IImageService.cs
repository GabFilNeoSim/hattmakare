namespace Hattmakare.Services;

public interface IImageService
{
    public Task<string?> UploadImageAsync(IFormFile file);
}
