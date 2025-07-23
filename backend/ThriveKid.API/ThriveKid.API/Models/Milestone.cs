using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThriveKid.API.Models
{
    public class Milestone
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        public DateTime AchievedDate { get; set; }

        // 🔗 Relationship to Child
        [ForeignKey("Child")]
        public int ChildId { get; set; }
        public Child? Child { get; set; }
    }
}
