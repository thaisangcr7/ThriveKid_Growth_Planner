using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThriveKid.API.Models
{
    public class FeedingLog
    {
        [Key]
        public int Id { get; set; }

        public DateTime FeedingTime { get; set; }

        public string MealType { get; set; } = string.Empty; // e.g., Breastmilk, Formula, Solid

        public string Notes { get; set; } = string.Empty;

        // 🔗 Relationship to Child
        [ForeignKey("Child")]
        public int ChildId { get; set; }
        public Child? Child { get; set; }
    }
}
