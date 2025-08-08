namespace ThriveKid.API.Models
{
    public enum RepeatRule
    {
        NONE = 0,
        DAILY = 1,
        WEEKLY = 2,
        MONTHLY = 3
    }

    public enum ReminderSource
    {
        Manual = 0,
        AutoAgeRule = 1
    }

    public class Reminder
    {
        public int Id { get; set; }

        public int ChildId { get; set; }
        public Child? Child { get; set; }

        public string Title { get; set; } = string.Empty;
        public string? Notes { get; set; }

        // Scheduling (all UTC)
        public DateTime DueAt { get; set; }          // first due time
        public RepeatRule RepeatRule { get; set; }   // NONE/DAILY/WEEKLY/MONTHLY

        // Engine bookkeeping
        public bool IsCompleted { get; set; }
        public DateTime? LastRunAt { get; set; }     // last time engine ran this reminder
        public DateTime? NextRunAt { get; set; }     // when engine should run next

        public ReminderSource Source { get; set; } = ReminderSource.Manual;
    }
}