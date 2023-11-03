using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NPWalks.API.Data;
using NPWalks.API.Models.Domain;
using NPWalks.API.Models.DTO;

namespace NPWalks.API.Repository
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NPWalksDBContext dBContext;

        public SQLRegionRepository(NPWalksDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<Region> CreateRegionAsync(Region region)
        {
            await dBContext.Regions.AddAsync(region);
            await dBContext.SaveChangesAsync();
            return region;

        }

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var region = await dBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (region == null)
            {
                return null;
            }

            dBContext.Regions.Remove(region);
            await dBContext.SaveChangesAsync();

            return region;
        }

        public async Task<List<Region>> GetAllRegionAsync()
        {
            return await dBContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetRegionAsync(Guid id)
        {
            return await dBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateRegionAsync(Guid id, Region regionDomain)
        {
            var region = await dBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (region == null)
            {
                return null;
            }

            region.Name = regionDomain.Name;
            region.Code = regionDomain.Code;
            region.RegionImageUrl = regionDomain.RegionImageUrl;


            await dBContext.SaveChangesAsync();

            return region;


        }
    }
}