namespace ThriveKid.API.DTOs.Reminders
{
    public class ReminderDto
    {
        public int Id { get; set; }
        public int ChildId { get; set; }
        public string ChildName { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;
        public string? Notes { get; set; }

        public DateTime DueAt { get; set; }
        public string RepeatRule { get; set; } = "NONE";
        public bool IsCompleted { get; set; }
        public DateTime? LastRunAt { get; set; }
        public DateTime? NextRunAt { get; set; }
        public string Source { get; set; } = "Manual";
    }
}