using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThriveKid.API.Models
{
    public class LearningGoal
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public bool IsCompleted { get; set; } = false;

        public DateTime? CompletedDate { get; set; }

        // Foreign key to Child
        public int ChildId { get; set; }

        [ForeignKey("ChildId")]
        public Child Child { get; set; } = null!;
    }
}
