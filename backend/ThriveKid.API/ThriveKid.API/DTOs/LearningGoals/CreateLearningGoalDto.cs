using System.ComponentModel.DataAnnotations;

namespace ThriveKid.API.DTOs.LearningGoals
{
    public class CreateLearningGoalDto
    {
        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public int ChildId { get; set; }
    }
}
