using Microsoft.EntityFrameworkCore;
using ThriveKid.API.Models;
using ThriveKid.API.Data;
using ThriveKid.API.Services;
using System;

// This class seeds initial data into the database
// It is called during application startup to ensure the database has some initial data
// This is useful for development and testing purposes

namespace ThriveKid.API.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<ThriveKidContext>(); // ✅ correct correct
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
                    Title = "First smile", // ✅ Updated property name
                    Notes = "She smiled during bath time.",
                    AchievedDate = new DateTime(2024, 1, 10),
                    ChildId = emma.Id
                },
                new Milestone
                {
                    Title = "Rolled over",
                    Notes = "Rolled on tummy time mat",
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
