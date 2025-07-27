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

            // Prevent reseeding
            if (context.Children.Any()) return;

            // STEP 1 – Seed Children and Save
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
            context.SaveChanges(); // 🛑 Save now to generate their IDs

            // STEP 2 – Seed Milestones using actual child Ids
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

            // STEP 3 – Seed FeedingLogs using actual child Ids
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
}
