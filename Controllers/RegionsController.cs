using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NPWalks.API.Data;
using NPWalks.API.Models.Domain;
using NPWalks.API.Models.DTO;

namespace NPWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : ControllerBase
    {
        private readonly NPWalksDBContext dbContext;

        public RegionsController(NPWalksDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Get All Regions
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get Data From Database - Domain model

            var regions = dbContext.Regions.ToList();
            // Map Data to DTOs
            var regionDto = new List<RegionDTO>();

            foreach (var region in regions)
            {
                regionDto.Add(new RegionDTO
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }
            // Return DTOs
            return Ok(regions);
        }

        // Get Single Region

        [HttpGet("{id:Guid}")]
        // [Route("{id:Guid}")]
        public IActionResult GetRegion(Guid id)
        {
            // Get Data From Database - Domain model

            // var region=dbContext.Regions.Find(id);
            var region = dbContext.Regions.FirstOrDefault(domainRegion => domainRegion.Id == id);

            // Domain Model to Region Dto
            var regionDto = new RegionDTO
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            if (regionDto == null)
            {
                return NotFound();
            }

            // Returnd Region DTOs
            return Ok(regionDto);
        }
    }
}