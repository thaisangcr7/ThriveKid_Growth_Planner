using Microsoft.EntityFrameworkCore; // Import EF Core for ORM/database access
using ThriveKid.API.Models;          // Import your application's data models
using ThriveKid.API.Data;            // (May be redundant here, since you're already in this namespace)

namespace ThriveKid.API.Data
{
    // The main database context for your application, inherits from EF Core's DbContext
    public class ThriveKidContext : DbContext
    {
        // Constructor: passes options (like connection string) to the base DbContext
        public ThriveKidContext(DbContextOptions<ThriveKidContext> options)
            : base(options)
        {
        }

        // DbSet properties represent tables in your database.
        // Each DbSet<T> allows you to query and save instances of T.
        public DbSet<Child> Children { get; set; } = null!; // Table for children
        public DbSet<Milestone> Milestones { get; set; }    // Table for milestones

        public DbSet<FeedingLog> FeedingLogs { get; set; }  // Table for feeding logs

        public DbSet<SleepLog> SleepLogs { get; set; }      // Table for sleep logs

        public DbSet<Reminder> Reminders { get; set; }      // Table for reminders

        public DbSet<LearningGoal> LearningGoals { get; set; } // Table for learning goals
        public DbSet<ToyRecommendation> ToyRecommendations { get; set; } // Table for toy recommendations

        // You can override OnModelCreating here if you need to customize table mappings, relationships, etc.
    }
}