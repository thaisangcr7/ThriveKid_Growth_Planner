namespace ThriveKid.API.DTOs.Reminders;

public class ReminderDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime ReminderTime { get; set; }
    public string? Notes { get; set; }
    public int ChildId { get; set; }
    public string? ChildName { get; set; }
}
