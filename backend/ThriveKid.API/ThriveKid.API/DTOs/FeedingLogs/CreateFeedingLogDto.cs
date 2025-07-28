using System.ComponentModel.DataAnnotations;

namespace ThriveKid.API.DTOs.FeedingLogs
{
    public class CreateFeedingLogDto
    {
        [Required]
        public DateTime FeedingTime { get; set; }

        [Required]
        public string MealType { get; set; }

        public string? Notes { get; set; }
    }
}
