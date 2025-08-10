using Microsoft.EntityFrameworkCore;
using ThriveKid.API.Data;
using ThriveKid.API.Models;

namespace ThriveKid.API
{
    // Static class for seeding initial data into the database
    public static class SeedData
    {
        // Call this method at app startup to ensure the DB has sample data
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ThriveKidContext(
                serviceProvider.GetRequiredService<DbContextOptions<ThriveKidContext>>());

            // Ensure database exists (optional if you always run migrations)
            // context.Database.EnsureCreated();

            // --- Seed Children ---
            // Only add children if the table is empty
            if (!context.Children.Any())
            {
                var emma = new Child
                {
                    FirstName = "Emma",
                    LastName  = "Nguyen",
                    // Treat DOB as UTC date; time portion irrelevant for DOB
                    DateOfBirth = new DateTime(2023, 10, 1, 0, 0, 0, DateTimeKind.Utc),
                    Gender = Gender.Female
                    // NOTE: Do NOT set AgeInMonths. We compute it in DTOs now.
                };

                var liam = new Child
                {
                    FirstName = "Liam",
                    LastName  = "Tran",
                    DateOfBirth = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc),
                    Gender = Gender.Male
                };

                context.Children.AddRange(emma, liam);
                context.SaveChanges(); // Save to generate IDs for later use
            }

            // Re-fetch by full name (safer than FirstName only)
            var emmaChild = context.Children.FirstOrDefault(c => c.FirstName == "Emma" && c.LastName == "Nguyen");
            var liamChild = context.Children.FirstOrDefault(c => c.FirstName == "Liam" && c.LastName == "Tran");

            // --- Seed Milestones ---
            if (!context.Milestones.Any() && emmaChild != null)
            {
                context.Milestones.AddRange(
                    new Milestone
                    {
                        Title = "First smile",
                        Notes = "She smiled during bath time.",
                        AchievedDate = new DateTime(2024, 1, 10, 0, 0, 0, DateTimeKind.Utc),
                        ChildId = emmaChild.Id
                    },
                    new Milestone
                    {
                        Title = "Rolled over",
                        Notes = "Rolled on tummy time mat",
                        AchievedDate = new DateTime(2024, 2, 15, 0, 0, 0, DateTimeKind.Utc),
                        ChildId = emmaChild.Id
                    }
                );
                context.SaveChanges();
            }

            // --- Seed FeedingLogs ---
            if (!context.FeedingLogs.Any() && emmaChild != null)
            {
                var now = DateTime.UtcNow;
                context.FeedingLogs.AddRange(
                    new FeedingLog
                    {
                        FeedingTime = now.AddHours(-2), // UTC
                        MealType = "Breastmilk",
                        Notes = "Fed well",
                        ChildId = emmaChild.Id
                    },
                    new FeedingLog
                    {
                        FeedingTime = now.AddHours(-1),
                        MealType = "Formula",
                        Notes = "Spit up slightly",
                        ChildId = emmaChild.Id
                    }
                );
                context.SaveChanges();
            }

            // --- Seed SleepLogs ---
            if (!context.SleepLogs.Any() && emmaChild != null && liamChild != null)
            {
                var now = DateTime.UtcNow;
                context.SleepLogs.AddRange(
                    new SleepLog
                    {
                        StartTime = now.AddHours(-6),
                        EndTime   = now.AddHours(-4),
                        Notes = "Afternoon nap after playtime.",
                        ChildId = emmaChild.Id
                    },
                    new SleepLog
                    {
                        StartTime = now.AddDays(-1).AddHours(-9),
                        EndTime   = now.AddDays(-1).AddHours(-6),
                        Notes = "Night sleep, uninterrupted.",
                        ChildId = liamChild.Id
                    }
                );
                context.SaveChanges();
            }

            // --- Seed Reminders ---
            if (!context.Reminders.Any() && emmaChild != null)
            {
                var now = DateTime.UtcNow;
                context.Reminders.Add(new Reminder
                {
                    ChildId = emmaChild.Id,
                    Title = "Vitamin D drop",
                    Notes = "Once daily",
                    DueAt = now.AddMinutes(1),
                    RepeatRule = RepeatRule.DAILY,
                    NextRunAt = now.AddMinutes(1),
                    IsCompleted = false,
                    Source = ReminderSource.Manual
                });
                context.SaveChanges();
            }

            // --- Seed LearningGoals ---
            if (!context.LearningGoals.Any() && emmaChild != null && liamChild != null)
            {
                context.LearningGoals.AddRange(
                    new LearningGoal
                    {
                        Title = "Learn to stack blocks",
                        IsCompleted = false,
                        ChildId = emmaChild.Id
                    },
                    new LearningGoal
                    {
                        Title = "Recognize colors",
                        IsCompleted = false,
                        ChildId = liamChild.Id
                    }
                );
                context.SaveChanges();
            }

            // --- Seed ToyRecommendations ---
            if (!context.ToyRecommendations.Any() && emmaChild != null && liamChild != null)
            {
                context.ToyRecommendations.AddRange(
                    new ToyRecommendation
                    {
                        ToyName = "Soft Rattle",
                        RecommendedAgeInMonths = 3,
                        Category = "Sensory",
                        ChildId = emmaChild.Id
                    },
                    new ToyRecommendation
                    {
                        ToyName = "Stacking Cups",
                        RecommendedAgeInMonths = 9,
                        Category = "Motor Skills",
                        ChildId = emmaChild.Id
                    },
                    new ToyRecommendation
                    {
                        ToyName = "Shape Sorter",
                        RecommendedAgeInMonths = 12,
                        Category = "Cognitive",
                        ChildId = liamChild.Id
                    },
                    new ToyRecommendation
                    {
                        ToyName = "Push Walker",
                        RecommendedAgeInMonths = 10,
                        Category = "Gross Motor",
                        ChildId = liamChild.Id
                    }
                );

                context.SaveChanges();
            }
        }
    }
}