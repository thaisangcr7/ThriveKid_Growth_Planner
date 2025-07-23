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
    }
}
