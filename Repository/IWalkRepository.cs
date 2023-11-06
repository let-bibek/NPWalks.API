using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPWalks.API.Models.Domain;

namespace NPWalks.API.Repository
{
    public interface IWalkRepository
    {
        Task<Walk> CreateWalkAsync(Walk walk);

        Task<List<Walk>> GetWalksAsync();
    }
}