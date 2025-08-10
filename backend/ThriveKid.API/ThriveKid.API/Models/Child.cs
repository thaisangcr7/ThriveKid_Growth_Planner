using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ThriveKid.API.Models
{
    public class Child
    {
        public int Id { get; set; }

        // core
        public string FirstName { get; set; } = string.Empty;
        public string LastName  { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; } = Gender.Unknown;

        // computed (not mapped)
        [NotMapped]
        public int AgeInMonths => ComputeAgeInMonths(DateOfBirth, DateTime.UtcNow);

        // navigation (kept but hidden in JSON to avoid cycles)
        [JsonIgnore] public ICollection<SleepLog> SleepLogs { get; set; } = new List<SleepLog>();
        [JsonIgnore] public ICollection<FeedingLog> FeedingLogs { get; set; } = new List<FeedingLog>();
        [JsonIgnore] public ICollection<LearningGoal> LearningGoals { get; set; } = new List<LearningGoal>();
        [JsonIgnore] public ICollection<ToyRecommendation> ToyRecommendations { get; set; } = new List<ToyRecommendation>();
        [JsonIgnore] public ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();

        public static int ComputeAgeInMonths(DateTime dobUtc, DateTime nowUtc)
        {
            var a = new DateTime(dobUtc.Year, dobUtc.Month, 1);
            var b = new DateTime(nowUtc.Year, nowUtc.Month, 1);
            return ((b.Year - a.Year) * 12) + b.Month - a.Month;
        }
    }
}