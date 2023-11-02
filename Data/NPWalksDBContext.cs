using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NPWalks.API.Models.Domain;

namespace NPWalks.API.Data
{
    public class NPWalksDBContext : DbContext
    {
        public NPWalksDBContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

    }


}