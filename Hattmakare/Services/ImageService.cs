using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Image = SixLabors.ImageSharp.Image;

namespace Hattmakare.Services
{
    public class ImageService : IImageService
    {
        public ImageService() { }

        public void DeleteImage(string fileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets/hats", fileName);
            if (File.Exists(filePath)) 
            {
                File.Delete(filePath);         
            }

        }

        public async Task<string?> UploadImageAsync(IFormFile file)
        {

            if (file == null)
            {
                return null;
            }
            string[] allowedExtensions = [".jpg", ".jpeg", ".png"];

            string extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                return null;
            }

            // Create upload folder
            string uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets/hats");
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            // Create a file name
            string fileName = Guid.NewGuid().ToString() + extension;
            string filePath = Path.Combine(uploadDirectory, fileName);

            // Resize
            using var image = await Image.LoadAsync(file.OpenReadStream());

            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(400, 400),
                Mode = ResizeMode.Crop
            }));

            // Save image to file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.SaveAsJpegAsync(fileStream);
            }

            return fileName;
        }
    }
}
