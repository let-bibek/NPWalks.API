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

        public async Task<List<Walk>> GetWalksAsync()
        {
            return await dBContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }
    }
}