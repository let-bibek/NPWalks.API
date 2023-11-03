using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPWalks.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}