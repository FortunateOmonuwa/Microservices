using System.ComponentModel.DataAnnotations;

namespace PlatformService.Models
{
    public class Platform
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "Name";
        [Required]
        public string Publisher { get; set; } = "Publisher";
        [Required]
        public string Cost { get; set; } = "Cost";
    }
}
