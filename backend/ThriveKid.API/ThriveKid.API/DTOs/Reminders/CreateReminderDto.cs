namespace ThriveKid.API.DTOs.Reminders
{
    public class CreateReminderDto
    {
        public int ChildId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public DateTime DueAt { get; set; }                // UTC
        public string RepeatRule { get; set; } = "NONE";   // NONE|DAILY|WEEKLY|MONTHLY
    }
}