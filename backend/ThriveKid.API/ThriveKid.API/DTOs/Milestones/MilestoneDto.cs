namespace ThriveKid.API.DTOs.Milestones
{
    public class MilestoneDto
    {
        public int Id { get; set; }
        public string Title { get; set; } // ✅ You were missing this!
        public string Notes { get; set; }
        public DateTime AchievedDate { get; set; }
        public int ChildId { get; set; } // FK
        public string ChildName { get; set; } // for display
    }

}
