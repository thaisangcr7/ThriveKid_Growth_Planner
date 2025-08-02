namespace ThriveKid.API.DTOs.SleepLogs
{
    public class SleepLogDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Notes { get; set; }
        public int ChildId { get; set; }
        public string ChildName { get; set; } // from navigation

        public double SleepDurationHours { get; set; }

    }
}
