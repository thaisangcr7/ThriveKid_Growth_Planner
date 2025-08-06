using System.ComponentModel.DataAnnotations;

namespace ThriveKid.API.DTOs.LearningGoals
{
    public class UpdateLearningGoalDto
    {
        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }

        public DateTime? CompletedDate { get; set; }
    }
}