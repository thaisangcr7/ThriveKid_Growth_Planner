namespace ThriveKid.API.DTOs.FeedingLogs
{
    public class FeedingLogDto
    {
        public int Id { get; set; }
        public DateTime FeedingTime { get; set; }
        public string MealType { get; set; }
        public string? Notes { get; set; }
        public int ChildId { get; set; }
        public string ChildName { get; set; }
    }
}
