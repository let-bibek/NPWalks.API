using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPWalks.API.Data;
using NPWalks.API.Models.Domain;

namespace NPWalks.API.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NPWalksDBContext dBContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, NPWalksDBContext dBContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dBContext = dBContext;
        }

        public async Task<Image> UploadImageAsync(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",

            $"{image.FileName}{image.FileExtension}"
            );

            // Upload the image to the local path
            using var stream = new FileStream(localFilePath, FileMode.Create);

            await image.File.CopyToAsync(stream);

            // https://localhost:8785/Images/demo.png

            var urlFilepath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilepath;

            // Save file name to the database

            await dBContext.Images.AddAsync(image);
            await dBContext.SaveChangesAsync();

            return image;

        }
    }
}