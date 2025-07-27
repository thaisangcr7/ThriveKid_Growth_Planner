using System.ComponentModel.DataAnnotations;

namespace ThriveKid.API.DTOs.Milestones
{
    public class CreateMilestoneDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        [Required]
        public DateTime AchievedDate { get; set; }
    }

}
