using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThriveKid.API.Models
{
    public class ToyRecommendation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ToyName { get; set; } = string.Empty;

        [Required]
        public int RecommendedAgeInMonths { get; set; }

        public string? Category { get; set; }

        // Foreign key to Child
        [ForeignKey("Child")]
        public int ChildId { get; set; }
        public Child? Child { get; set; }
    }
}