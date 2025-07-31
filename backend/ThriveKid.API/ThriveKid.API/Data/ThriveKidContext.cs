using Microsoft.EntityFrameworkCore;
using ThriveKid.API.Models;

namespace ThriveKid.API.Data
{
    public class ThriveKidContext : DbContext
    {
        public ThriveKidContext(DbContextOptions<ThriveKidContext> options)
            : base(options)
        {
        }
        public DbSet<Child> Children { get; set; } = null!;
        public DbSet<Milestone> Milestones { get; set; }

        public DbSet<FeedingLog> FeedingLogs { get; set; }

        public DbSet<SleepLog> SleepLogs { get; set; }


    }
}
