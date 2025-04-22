namespace Hattmakare.Services;

public interface IImageService
{
    public Task<string?> UploadImageAsync(IFormFile file);
    public void DeleteImage(string fileName);
}
