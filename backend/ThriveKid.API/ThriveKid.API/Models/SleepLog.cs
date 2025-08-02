using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThriveKid.API.Models
{
    public class SleepLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; } 

        [Required]
        public DateTime EndTime { get; set; }

        public string Notes { get; set; }

        // Foreign key
        public int ChildId { get; set; }

        [ForeignKey("ChildId")]
        public Child Child { get; set; }

        [NotMapped]
        public TimeSpan SleepDuration => EndTime - StartTime;

    }
}
