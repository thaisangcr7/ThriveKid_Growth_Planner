namespace ThriveKid.API.DTOs.Reminders
{
    public class CreateReminderDto
    {
        public string Title { get; set; } = string.Empty;
        public DateTime ReminderTime { get; set; }
        public string? Notes { get; set; }
        public int ChildId { get; set; }
    }
}
