namespace ThriveKid.API.DTOs.SleepLogs
{
    public class CreateSleepLogDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Notes { get; set; }
        public int ChildId { get; set; }
    }
}
