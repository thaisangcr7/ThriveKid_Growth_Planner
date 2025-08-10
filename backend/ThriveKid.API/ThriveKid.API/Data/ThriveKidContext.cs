using Microsoft.EntityFrameworkCore; // Import EF Core for ORM/database access
using ThriveKid.API.Models;          // Import your application's data models


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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Child entity
            // This is where you define how the Child model maps to the database table
            var child = modelBuilder.Entity<Child>();

            // Store Gender enum as TEXT (your SQLite column is already TEXT)
            child.Property(c => c.Gender)
                 .HasConversion<string>()
                 .HasMaxLength(16);

            // AgeInMonths is computed in code, not mapped to DB
            // This means it won't be stored in the database, but can be calculated on the fly
            // when you retrieve Child entities.
            // This is useful for performance and storage efficiency.
            // If you want to store it in the database, you would need to add a column in the database schema.
            // However, since it's a computed property, we don't map it to the database.
            // This is done to avoid cycles in JSON serialization and to keep the database schema clean.
            child.Ignore(c => c.AgeInMonths);

            // (Optional) explicit relationships — safe to skip now since FKs exist
            // child.HasMany(c => c.SleepLogs).WithOne(sl => sl.Child).HasForeignKey(sl => sl.ChildId);
            // child.HasMany(c => c.FeedingLogs).WithOne(fl => fl.Child).HasForeignKey(fl => fl.ChildId);
            // child.HasMany(c => c.LearningGoals).WithOne(lg => lg.Child).HasForeignKey(lg => lg.ChildId);
            // child.HasMany(c => c.ToyRecommendations).WithOne(tr => tr.Child).HasForeignKey(tr => tr.ChildId);
            // child.HasMany(c => c.Reminders).WithOne(r => r.Child).HasForeignKey(r => r.ChildId);
            // 🔼🔼 ADD THIS BLOCK EXACTLY ONCE 🔼🔼

            // ...keep any other existing model config you already have
        }
    }
}