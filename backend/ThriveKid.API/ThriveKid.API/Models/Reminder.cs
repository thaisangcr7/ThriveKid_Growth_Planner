using ThriveKid.API.Models;

public class Reminder
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime ReminderTime { get; set; }
    public string? Notes { get; set; }
    public int ChildId { get; set; }
    public Child? Child { get; set; }
}

