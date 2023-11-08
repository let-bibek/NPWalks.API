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
        public NPWalksDBContext(DbContextOptions<NPWalksDBContext> dbContextOptions) : base(dbContextOptions)
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


            // Seed difficulties into Difficulties Table
            modelBuilder.Entity<Difficulty>().HasData(difficulties);



            // Seed data into Regions table
            var regions = new List<Region>(){
                new Region{
                        Id=Guid.Parse("c4dc6da0-01b4-449b-a883-eb80540c377b"),
                        Code="KTM",
                        Name="Kathmandu",
                        RegionImageUrl="images/kathmandu.png"
                },
                 new Region{
                        Id=Guid.Parse("e87999c6-345e-4741-a109-0f09ae53a558"),
                        Code="PKR",
                        Name="Pokhara",
                        RegionImageUrl="images/pokhara.png"
                },
                 new Region{
                        Id=Guid.Parse("d62652a9-f70c-40c8-9b63-24e7a0c945cc"),
                        Code="DHG",
                        Name="Dhading",
                        RegionImageUrl="images/dhading.png"
                },
                 new Region{
                        Id=Guid.Parse("6976f82d-d489-4d6a-ac79-ce834574fda8"),
                        Code="BKT",
                        Name="Bhaktapur",
                        RegionImageUrl="images/Bhaktapur.png"
                },
                 new Region{
                        Id=Guid.Parse("60deef7b-d42d-4adc-85a7-248436c36ea2"),
                        Code="LTPR",
                        Name="Lalitpur",
                        RegionImageUrl="images/lalitpur.png"
                },
                 new Region{
                        Id=Guid.Parse("a4db467f-36e7-42a4-b2ee-1df41d0addb6"),
                        Code="NKWT",
                        Name="Nuwakot",
                        RegionImageUrl="images/nuwakot.png"
                },
                 new Region{
                        Id=Guid.Parse("75916009-756f-43da-80f4-a08506c14e95"),
                        Code="CHTWN",
                        Name="Chitwan",
                        RegionImageUrl="images/chitwan.png"
                },
                 new Region{
                        Id=Guid.Parse("75916009-756f-43da-80f4-a08506c14e85"),
                        Code="NWPR",
                        Name="Nawalpur",
                        RegionImageUrl="images/nawalpur.png"
                },
            };

            // Seed regions into Regions Table
            modelBuilder.Entity<Region>().HasData(regions);
        }

    }


}