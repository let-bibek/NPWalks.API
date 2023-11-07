using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Services;
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
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkRequestDto addWalkRequestDto)
        {

            if (ModelState.IsValid)
            {
                // Walk DTO to Walk model mapping
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

                walkDomainModel = await walkRepository.CreateWalkAsync(walkDomainModel);

                var walkDto = mapper.Map<WalkDto>(walkDomainModel);

                return Ok(walkDto);
            }
            else
            {
                return BadRequest(ModelState);
            }


        }

        // Get Walks
        [HttpGet]
        public async Task<IActionResult> GetWalks()
        {
            var walksDomainModel = await walkRepository.GetWalksAsync();

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

        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            if (ModelState.IsValid)
            {
                var updateDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

                var walkdomainModel = await walkRepository.UpdateWalkAsync(id, updateDomainModel);

                if (walkdomainModel == null)
                {
                    return NotFound();
                }

                return Ok(mapper.Map<WalkDto>(walkdomainModel));
            }
            else
            {
                return BadRequest(ModelState);
            }
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