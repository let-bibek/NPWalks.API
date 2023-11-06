using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPWalks.API.Data;
using NPWalks.API.Models.Domain;
using NPWalks.API.Models.DTO;
using NPWalks.API.Repository;

namespace NPWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {

            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // Get All Regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Data From Database - Domain model
            var regions = await regionRepository.GetAllRegionAsync();

            // Map Data to DTOs
            // var regionDto = new List<RegionDTO>();

            // foreach (var region in regions)
            // {
            //     regionDto.Add(new RegionDTO
            //     {
            //         Id = region.Id,
            //         Code = region.Code,
            //         Name = region.Name,
            //         RegionImageUrl = region.RegionImageUrl
            //     });
            // }

            // Return DTOs
            return Ok(mapper.Map<List<RegionDTO>>(regions));
        }


        // Get Single Region

        [HttpGet("{id:Guid}")]
        // [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegion([FromRoute] Guid id)
        {
            // Get Data From Database - Domain model

            // var region=dbContext.Regions.Find(id);
            var region = await regionRepository.GetRegionAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            // Domain Model to Region Dto
            // var regionDto = new RegionDTO
            // {
            //     Id = region.Id,
            //     Code = region.Code,
            //     Name = region.Name,
            //     RegionImageUrl = region.RegionImageUrl
            // };

            // Return Region DTOs
            return Ok(mapper.Map<RegionDTO>(region));
        }


        // Create a new Region

        [HttpPost]

        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            // Map DTO to domain model

            // var regionDomainModel = new Region
            // {
            //     Name = addRegionRequestDto.Name,
            //     Code = addRegionRequestDto.Code,
            //     RegionImageUrl = addRegionRequestDto.RegionImageUrl
            // };

            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);


            // Use domain model to create new Region
            regionDomainModel = await regionRepository.CreateRegionAsync(regionDomainModel);

            // Map domain model back to DTO

            // var regionDto = new RegionDTO
            // {
            //     Id = regionDomainModel.Id,
            //     Name = regionDomainModel.Name,
            //     Code = regionDomainModel.Code,
            //     RegionImageUrl = regionDomainModel.RegionImageUrl
            // };

            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);


            return CreatedAtAction(nameof(GetRegion), new { id = regionDto.Id }, regionDto);
        }

        // Update the region   

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {

            // var regionDomainModel = new Region
            // {
            //     Name = updateRegionRequestDTO.Name,
            //     Code = updateRegionRequestDTO.Code,
            //     RegionImageUrl = updateRegionRequestDTO.RegionImageUrl
            // };

            // DTO to Domain Model

            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDTO);


            regionDomainModel = await regionRepository.UpdateRegionAsync(id, regionDomainModel);
            // Check if the region exists
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Domain model to DTO and return it

            // var regionDto = new RegionDTO
            // {
            //     Id = regionDomainModel.Id,
            //     Name = regionDomainModel.Name,
            //     Code = regionDomainModel.Code,
            //     RegionImageUrl = regionDomainModel.RegionImageUrl
            // };

            return Ok(mapper.Map<RegionDTO>(regionDomainModel));

        }

        // Delete a region
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteRegionAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();

            }

            // Create DTO for the region domain
            // var regionDto = new RegionDTO
            // {
            //     Id = regionDomainModel.Id,
            //     Code = regionDomainModel.Code,
            //     Name = regionDomainModel.Name,
            //     RegionImageUrl = regionDomainModel.RegionImageUrl
            // };

            return Ok(mapper.Map<RegionDTO>(regionDomainModel));
        }
    }
}