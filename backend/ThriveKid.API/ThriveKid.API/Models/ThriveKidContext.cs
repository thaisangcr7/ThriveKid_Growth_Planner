using Microsoft.EntityFrameworkCore;

namespace ThriveKid.API.Models
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



    }
}
