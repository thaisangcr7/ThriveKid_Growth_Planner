using Microsoft.EntityFrameworkCore;
using ThriveKid.API.Models;

namespace ThriveKid.API
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ThriveKidContext(
                serviceProvider.GetRequiredService<DbContextOptions<ThriveKidContext>>());

            // Prevent reseeding if any data already exists
            if (context.Children.Any()) return;

            SeedChildren(context);
            // Future:
            // SeedMilestones(context);
            // SeedFeedingLogs(context);

            context.SaveChanges();
        }

        private static void SeedChildren(ThriveKidContext context)
        {
            context.Children.AddRange(
                new Child
                {
                    FirstName = "Emma",
                    LastName = "Nguyen",
                    DateOfBirth = new DateTime(2023, 10, 1),
                    Gender = "Female",
                    AgeInMonths = 21
                },
                new Child
                {
                    FirstName = "Liam",
                    LastName = "Tran",
                    DateOfBirth = new DateTime(2024, 1, 15),
                    Gender = "Male",
                    AgeInMonths = 18
                }
            );
        }

        // Placeholder for Milestone, FeedingLog, etc.
        private static void SeedMilestones(ThriveKidContext context)
        {
            var emma = context.Children.FirstOrDefault(c => c.FirstName == "Emma");
            var olivia = context.Children.FirstOrDefault(c => c.FirstName == "Olivia");

            if (emma != null)
            {
                context.Milestones.AddRange(
                    new Milestone
                    {
                        Description = "First smile",
                        AchievedDate = new DateTime(2024, 1, 10),
                        ChildId = emma.Id
                    },
                    new Milestone
                    {
                        Description = "Rolled over",
                        AchievedDate = new DateTime(2024, 2, 15),
                        ChildId = emma.Id
                    }
                );
            }

            if (olivia != null)
            {
                context.Milestones.Add(
                    new Milestone
                    {
                        Description = "Started crawling",
                        AchievedDate = new DateTime(2024, 3, 20),
                        ChildId = olivia.Id
                    }
                );
            }
        }

        private static void SeedFeedingLogs(ThriveKidContext context)
        {
            var emma = context.Children.FirstOrDefault(c => c.FirstName == "Emma");
            var olivia = context.Children.FirstOrDefault(c => c.FirstName == "Olivia");

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
            }

            if (olivia != null)
            {
                context.FeedingLogs.Add(
                    new FeedingLog
                    {
                        FeedingTime = DateTime.Now.AddHours(-3),
                        MealType = "Solid",
                        Notes = "Ate banana mash",
                        ChildId = olivia.Id
                    }
                );
            }
        }
            // Note: Ensure to add any additional seeding methods for other models as needed.
    }
}
