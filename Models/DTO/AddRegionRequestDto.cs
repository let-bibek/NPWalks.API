using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NPWalks.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage ="Minimun length required is 3")]
        [MaxLength(10, ErrorMessage ="Maximum length 10 is exceeded")]
        public string Code { get; set; }

        [Required]
        [MinLength(4, ErrorMessage ="Minimum length required is 4")]
        [MaxLength(100, ErrorMessage ="Maximum length 100 is exceeded")]
        public string Name { get; set; }
        
        public string? RegionImageUrl { get; set; }
    }
}