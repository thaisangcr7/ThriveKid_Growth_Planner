using Microsoft.EntityFrameworkCore; // Enables EF Core DB operations
using ThriveKid.API.Data;            // For ThriveKidContext (your DB context)
using ThriveKid.API.Models;          // For your entity models

namespace ThriveKid.API
{
    // Static class for seeding initial data into the database
    public static class SeedData
    {
        // Call this method at app startup to ensure the DB has sample data
        public static void Initialize(IServiceProvider serviceProvider)
        {
            // Create a new DB context using dependency injection
            using var context = new ThriveKidContext(
                serviceProvider.GetRequiredService<DbContextOptions<ThriveKidContext>>());

            // --- Seed Children ---
            // Only add children if the table is empty
            if (!context.Children.Any())
            {
                var emma = new Child
                {
                    FirstName = "Emma",
                    LastName = "Nguyen",
                    DateOfBirth = new DateTime(2023, 10, 1),
                    Gender = "Female",
                    AgeInMonths = 21
                };

                var liam = new Child
                {
                    FirstName = "Liam",
                    LastName = "Tran",
                    DateOfBirth = new DateTime(2024, 1, 15),
                    Gender = "Male",
                    AgeInMonths = 18
                };

                context.Children.AddRange(emma, liam);
                context.SaveChanges(); // Save to generate IDs for later use
            }

            // --- Seed Milestones ---
            // Only add milestones if the table is empty
            if (!context.Milestones.Any())
            {
                var emma = context.Children.FirstOrDefault(c => c.FirstName == "Emma");
                if (emma != null)
                {
                    context.Milestones.AddRange(
                        new Milestone
                        {
                            Title = "First smile",
                            Notes = "She smiled during bath time.",
                            AchievedDate = new DateTime(2024, 1, 10),
                            ChildId = emma.Id // Link to Emma
                        },
                        new Milestone
                        {
                            Title = "Rolled over",
                            Notes = "Rolled on tummy time mat",
                            AchievedDate = new DateTime(2024, 2, 15),
                            ChildId = emma.Id
                        }
                    );
                    context.SaveChanges();
                }
            }

            // --- Seed FeedingLogs ---
            // Only add feeding logs if the table is empty
            if (!context.FeedingLogs.Any())
            {
                var emma = context.Children.FirstOrDefault(c => c.FirstName == "Emma");
                if (emma != null)
                {
                    context.FeedingLogs.AddRange(
                        new FeedingLog
                        {
                            FeedingTime = DateTime.Now.AddHours(-2),
                            MealType = "Breastmilk",
                            Notes = "Fed well",
                            ChildId = emma.Id
                        },
                        new FeedingLog
                        {
                            FeedingTime = DateTime.Now.AddHours(-1),
                            MealType = "Formula",
                            Notes = "Spit up slightly",
                            ChildId = emma.Id
                        }
                    );
                    context.SaveChanges();
                }
            }

            // --- Seed SleepLogs ---
            // Only add sleep logs if the table is empty
            if (!context.SleepLogs.Any())
            {
                var emma = context.Children.FirstOrDefault(c => c.FirstName == "Emma");
                var liam = context.Children.FirstOrDefault(c => c.FirstName == "Liam");
                if (emma != null && liam != null)
                {
                    context.SleepLogs.AddRange(
                        new SleepLog
                        {
                            StartTime = DateTime.Now.AddHours(-6),
                            EndTime = DateTime.Now.AddHours(-4),
                            Notes = "Afternoon nap after playtime.",
                            ChildId = emma.Id
                        },
                        new SleepLog
                        {
                            StartTime = DateTime.Now.AddDays(-1).AddHours(-9),
                            EndTime = DateTime.Now.AddDays(-1).AddHours(-6),
                            Notes = "Night sleep, uninterrupted.",
                            ChildId = liam.Id
                        }
                    );
                    context.SaveChanges();
                }
            }

            // --- Seed Reminders ---
            // Only add reminders if the table is empty
            if (!context.Reminders.Any())
            {
                var emma = context.Children.FirstOrDefault(c => c.FirstName == "Emma");
                if (emma != null)
                {
                    context.Reminders.Add(new Reminder
                    {
                        ChildId = emma.Id,
                        Title = "Vitamin D drop",
                        Notes = "Once daily",
                        DueAt = DateTime.UtcNow.AddMinutes(1),
                        RepeatRule = RepeatRule.DAILY,
                        NextRunAt = DateTime.UtcNow.AddMinutes(1),
                        IsCompleted = false,
                        Source = ReminderSource.Manual
                    });
                    context.SaveChanges();
                }
            }

            // --- Seed LearningGoals ---
            // Only add learning goals if the table is empty
            if (!context.LearningGoals.Any())
            {
                // Re-query children to get their generated IDs
                var emma = context.Children.First(c => c.FirstName == "Emma");
                var liam = context.Children.First(c => c.FirstName == "Liam");

                context.LearningGoals.AddRange(
                    new LearningGoal
                    {
                        Title = "Learn to stack blocks",
                        IsCompleted = false,
                        ChildId = emma.Id
                    },
                    new LearningGoal
                    {
                        Title = "Recognize colors",
                        IsCompleted = false,
                        ChildId = liam.Id
                    }
                );
                context.SaveChanges();
            }

            // --- Seed ToyRecommendations ---
            // Only add toy recommendations if the table is empty
            if (!context.ToyRecommendations.Any())
            {
                var emma = context.Children.FirstOrDefault(c => c.FirstName == "Emma");
                var liam = context.Children.FirstOrDefault(c => c.FirstName == "Liam");

                if (emma != null && liam != null)
                {
                    context.ToyRecommendations.AddRange(
                        new ToyRecommendation
                        {
                            ToyName = "Soft Rattle",
                            RecommendedAgeInMonths = 3,
                            Category = "Sensory",
                            ChildId = emma.Id
                        },
                        new ToyRecommendation
                        {
                            ToyName = "Stacking Cups",
                            RecommendedAgeInMonths = 9,
                            Category = "Motor Skills",
                            ChildId = emma.Id
                        },
                        new ToyRecommendation
                        {
                            ToyName = "Shape Sorter",
                            RecommendedAgeInMonths = 12,
                            Category = "Cognitive",
                            ChildId = liam.Id
                        },
                        new ToyRecommendation
                        {
                            ToyName = "Push Walker",
                            RecommendedAgeInMonths = 10,
                            Category = "Gross Motor",
                            ChildId = liam.Id
                        }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}