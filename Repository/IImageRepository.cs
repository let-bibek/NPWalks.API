using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPWalks.API.Models.Domain;

namespace NPWalks.API.Repository
{
    public interface IImageRepository
    {
        Task<Image> UploadImageAsync(Image image);
    }
}