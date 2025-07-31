namespace ThriveKid.API.DTOs.SleepLogs
{
    public class UpdateSleepLogDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Notes { get; set; }

    }
}
