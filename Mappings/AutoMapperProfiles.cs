using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NPWalks.API.Models.Domain;
using NPWalks.API.Models.DTO;

namespace NPWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // For mapping betweeen region model to RegionDTO and vice versa
            CreateMap<Region, RegionDTO>().ReverseMap();

            // For mapping betweeen region model to AddRegionRequestDto and vice versa
            CreateMap<Region, AddRegionRequestDto>().ReverseMap();

            // For mapping betweeen region model to UpdateRegionRequestDTO and vice versa
            CreateMap<Region, UpdateRegionRequestDTO>().ReverseMap();
        }
    }
}