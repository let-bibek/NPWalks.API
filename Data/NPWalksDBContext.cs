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
        public NPWalksDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }


        // Data Seeding
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data into Difficulties
            // Easy, Medium and Hard

            var difficulties = new List<Difficulty>(){
                    new Difficulty{
                        Id=Guid.Parse("335367c5-70b1-4d7f-9163-f181a8f30a4c"),
                        Name="Easy"
                    },
                     new Difficulty{
                        Id=Guid.Parse("a04cf69b-9964-4cf6-b9a1-1767e6b2f406"),
                        Name="Medium"
                    },
                     new Difficulty{
                        Id=Guid.Parse("89a57987-2e21-46d7-8a20-40fd8b3dab52"),
                        Name="Hard"
                    }
            };
        }

    }


}