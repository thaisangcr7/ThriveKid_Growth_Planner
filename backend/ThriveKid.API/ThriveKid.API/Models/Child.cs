using System.ComponentModel.DataAnnotations;

namespace ThriveKid.API.Models
{
    public class Child
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50, ErrorMessage = "First name muist be under 50 characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(50, ErrorMessage = "Last name must be under 50 characters.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        public DateTime DateOfBirth { get; set; }

        public string? Gender { get; set; }

        [Range(0, 240, ErrorMessage = "Age in months must be between 0 and 240.")]
        public int AgeInMonths { get; set; }

        public ICollection<Milestone> Milestones { get; set; } = new List<Milestone>();

        public ICollection<FeedingLog> FeedingLogs { get; set; } = new List<FeedingLog>();

    }
}
