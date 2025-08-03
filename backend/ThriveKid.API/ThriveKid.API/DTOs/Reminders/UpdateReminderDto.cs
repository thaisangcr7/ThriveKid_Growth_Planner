namespace ThriveKid.API.DTOs.Reminders
{
    public class UpdateReminderDto
    {
        public string Title { get; set; } = string.Empty;
        public DateTime ReminderTime { get; set; }
        public string? Notes { get; set; }
    }
}
