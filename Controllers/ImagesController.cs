using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NPWalks.API.Models.Domain;
using NPWalks.API.Models.DTO;
using NPWalks.API.Repository;

namespace NPWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        // POST: /api/Images/Upload

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadImage([FromForm] AddImageRequestDto addImageRequestDto)
        {

            validateImageUpload(addImageRequestDto);

            if (ModelState.IsValid)
            {
                // Convert DTO to Domain Model
                var imageDomainModel = new Image
                {
                    File = addImageRequestDto.File,
                    FileName = addImageRequestDto.FileName,
                    FileDescription = addImageRequestDto.FileDescription,
                    FileExtension = Path.GetExtension(addImageRequestDto.File.FileName),
                    FileSizeInBytes = addImageRequestDto.File.Length
                };

                var imageRequest = await imageRepository.UploadImageAsync(imageDomainModel);
                return Ok(imageDomainModel);

                // Upload the image
            }

            return BadRequest(ModelState);
        }

        private void validateImageUpload(AddImageRequestDto addImageRequestDto)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".gif", ".bmp", ".png", ".tiff" };

            if (!allowedExtensions.Contains(Path.GetExtension(addImageRequestDto.File.FileName)))
            {
                ModelState.AddModelError("file", "The uploaded file is not supported");
            }

            if (addImageRequestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "The Uploaded file is too big");
            }
        }
    }
}