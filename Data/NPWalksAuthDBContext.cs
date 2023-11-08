using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NPWalks.API.Data
{
    public class NPWalksAuthDBContext : IdentityDbContext
    {
        public NPWalksAuthDBContext(DbContextOptions<NPWalksAuthDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "e11f4348-f981-4f85-9e59-6deccf18187b";
            var writerRoleId = "2912f79a-4e8d-47a8-8d86-a3635378b4c1";
            var roles = new List<IdentityRole>{
                new IdentityRole{
                    Id=readerRoleId,
                    ConcurrencyStamp=readerRoleId,
                    Name="Reader",
                    NormalizedName="reader".ToUpper(),

                },

                new IdentityRole{
                    Id=writerRoleId,
                    ConcurrencyStamp=writerRoleId,
                    Name="Writer",
                    NormalizedName="writer".ToUpper(),
                }

            };

            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}