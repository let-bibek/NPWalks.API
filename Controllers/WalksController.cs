using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Services;
using NPWalks.API.CustomActionFilters;
using NPWalks.API.Models.Domain;
using NPWalks.API.Models.DTO;
using NPWalks.API.Repository;

namespace NPWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        // Create Walk
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkRequestDto addWalkRequestDto)
        {

            // Walk DTO to Walk model mapping
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            walkDomainModel = await walkRepository.CreateWalkAsync(walkDomainModel);

            var walkDto = mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkDto);

        }

        // Get Walks
        // GET: api/walks?filterOn=Name&filterQuery=kathmandu?sortBy=Name&isAsc=true
        [HttpGet]
        public async Task<IActionResult> GetWalks([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
        [FromQuery] string? sortBy, [FromQuery] bool? isAsc)
        {
            var walksDomainModel = await walkRepository.GetWalksAsync(filterOn, filterQuery, sortBy, isAsc ?? true);

            if (walksDomainModel == null)
            {
                return NotFound();
            }

            var walkDto = mapper.Map<List<WalkDto>>(walksDomainModel);

            return Ok(walkDto);
        }

        // Get Walk
        [HttpGet("{id:Guid}")]

        public async Task<IActionResult> GetWalk([FromRoute] Guid id)
        {

            var walkDomainModel = await walkRepository.GetWalkAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        // Update Walk
        [HttpPut("{id:Guid}")]
        [ValidateModel]

        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {

            var updateDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            var walkdomainModel = await walkRepository.UpdateWalkAsync(id, updateDomainModel);

            if (walkdomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkdomainModel));


        }

        // Delete Walk
        [HttpDelete("{id:Guid}")]


        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var walkdomainModel = await walkRepository.DeleteWalkAsync(id);

            if (walkdomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkdomainModel));
        }

    }
}