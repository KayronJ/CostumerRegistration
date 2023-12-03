namespace CostumerRegistration.Services.ImageHelper
{
    public class ImageHelper
    {
        public async Task<string> SaveImageAsync(IFormFile file, IWebHostEnvironment hostingEnvironment)
        {
            if (file != null && file.Length > 0 && Path.GetExtension(file.FileName).Equals(".png", StringComparison.OrdinalIgnoreCase))
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Storage");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return Path.Combine("Storage", uniqueFileName);
            }

            return null;
        }

    }
}
