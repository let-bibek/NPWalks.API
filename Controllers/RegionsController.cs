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
        public IActionResult GetRegion([FromRoute] Guid id)
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


        // Create a new Region

        [HttpPost]

        public IActionResult CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            // Map DTO to domain model
            var regionDomainModel = new Region
            {
                Name = addRegionRequestDto.Name,
                Code = addRegionRequestDto.Code,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };


            // Use Domain Model to Create Region
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            // Map domain model back to DTO
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetRegion), new { id = regionDto.Id }, regionDto);
        }

        // Update the region   

        [HttpPut("{id:Guid}")]
        public IActionResult UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            // Get the region
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            // Check if the region exists
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to domain
            regionDomainModel.Code = updateRegionRequestDTO.Code;
            regionDomainModel.Name = updateRegionRequestDTO.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDTO.RegionImageUrl;

            dbContext.SaveChanges();

            // Domain model to DTO
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);

        }

        // Delete a region
        [HttpDelete("{id:Guid}")]
        public IActionResult DeleteRegion([FromRoute] Guid id)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();

            }

            // Delete the region domain

            dbContext.Regions.Remove(regionDomainModel);
            dbContext.SaveChanges();

            // Create DTO for the region domain
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);
        }
    }
}