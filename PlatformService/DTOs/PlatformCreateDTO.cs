using System.ComponentModel.DataAnnotations;

namespace PlatformService.DTOs
{
    public class PlatformCreateDTO
    {
    
        [Required]
        public string Name { get; set; } = "Name";
        [Required]
        public string Publisher { get; set; } = "Publisher";
        [Required]
        public string Cost { get; set; } = "Cost";
    }
}
