using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NPWalks.API.Data;
using NPWalks.API.Models.Domain;

namespace NPWalks.API.Repository
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NPWalksDBContext dBContext;

        public SQLWalkRepository(NPWalksDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            await dBContext.Walks.AddAsync(walk);
            await dBContext.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk?> DeleteWalkAsync(Guid id)
        {
            var walkDomainModel = await dBContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (walkDomainModel == null)
            {
                return null;
            }

            dBContext.Walks.Remove(walkDomainModel);
            await dBContext.SaveChangesAsync();

            return walkDomainModel;
        }

        public async Task<Walk> GetWalkAsync(Guid id)
        {
            // Here Include("Difficulty").Include("Region") is used to show the informations of Region and Difficulty models
            // in the Walk 
            var walk = await dBContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null)
            {
                return null;
            }
            return walk;
        }

        public async Task<List<Walk>> GetWalksAsync(string? queryOn = null, string? queryString = null,
        string? sortBy = null, bool isAsc = true, int pageNumber = 1, int pageSize = 1000
        )
        {
            var walks = dBContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            // Filtering 
            if (string.IsNullOrWhiteSpace(queryString) == false && string.IsNullOrWhiteSpace(queryOn) == false)
            {
                if (queryOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(queryString));
                }

                if (queryOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Description.Contains(queryString));
                }
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                // Order by Name
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAsc ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                // Order by Length
                else if (sortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAsc ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();

            // return await dBContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> UpdateWalkAsync(Guid id, Walk walk)
        {
            var walkDomainModel = await dBContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walkDomainModel == null)
            {

                return null;
            }

            walkDomainModel.Name = walk.Name;
            walkDomainModel.Description = walk.Description;
            walkDomainModel.LengthInKm = walk.LengthInKm;
            walkDomainModel.WalkImageUrl = walk.WalkImageUrl;
            walkDomainModel.RegionId = walk.RegionId;
            walkDomainModel.DifficultyId = walk.DifficultyId;

            await dBContext.SaveChangesAsync();

            return walkDomainModel;
        }
    }
}