using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThriveKid.API.Models
{
    public class FeedingLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime FeedingTime { get; set; }

        [Required]
        public string MealType { get; set; }  // e.g., Breastmilk, Formula, Solid

        public string? Notes { get; set; }

        // Foreign Key Relationship to Child
        [ForeignKey("Child")]
        public int ChildId { get; set; }

        public Child Child { get; set; }
    }
}
