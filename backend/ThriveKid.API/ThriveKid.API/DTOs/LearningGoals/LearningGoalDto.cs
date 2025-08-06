using System;

namespace ThriveKid.API.DTOs.LearningGoals
{
    public class LearningGoalDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int ChildId { get; set; }
        public string ChildName { get; set; } = string.Empty;
    }
}