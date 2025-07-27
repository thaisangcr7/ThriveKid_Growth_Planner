namespace ThriveKid.API.DTOs.Milestones
{
    public class UpdateMilestoneDto
    {
        public string Title { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime AchievedDate { get; set; }
        
    }
}
