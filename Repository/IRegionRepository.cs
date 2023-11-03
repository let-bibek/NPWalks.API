using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPWalks.API.Models.Domain;
using NPWalks.API.Models.DTO;

namespace NPWalks.API.Repository
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegionAsync();

        Task<Region?> GetRegionAsync(Guid id);

        Task<Region> CreateRegionAsync(Region region);

        Task<Region?> UpdateRegionAsync(Guid id, Region region);

        Task<Region?> DeleteRegionAsync(Guid id);
    }
}