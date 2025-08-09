using Microsoft.EntityFrameworkCore; // For database access
using Microsoft.Extensions.Hosting;  // For BackgroundService base class
using ThriveKid.API.Data;            // For ThriveKidContext (EF Core DB context)
using ThriveKid.API.Models;          // For Reminder, RepeatRule, etc.
using ThriveKid.API.Services.Engines.AgeRules; // For age-based reminder rules

namespace ThriveKid.API.Services.Engines
{
    // ReminderEngine runs as a background service in your app
    public class ReminderEngine : BackgroundService
    {
        private readonly IServiceProvider _services; // Used to create scoped services (like DB context)

        // How often to poll for due reminders (every 60 seconds)
        private readonly TimeSpan _pollInterval = TimeSpan.FromSeconds(60);

        // When to run age-based rules daily (2:00 AM Eastern)
        private readonly TimeSpan _dailyRuleTimeEST = new(2, 0, 0);

        public ReminderEngine(IServiceProvider services) => _services = services;

        // Main loop: runs until the app shuts down
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var dailyRuleAnchor = EasternTodayAt(_dailyRuleTimeEST);

            while (!stoppingToken.IsCancellationRequested)
            {
                var nowUtc = DateTime.UtcNow;

                // Process reminders that are due
                await ProcessDueReminders(nowUtc, stoppingToken);

                // Run age-based rules once per day after the anchor time
                if (nowUtc >= dailyRuleAnchor)
                {
                    await ApplyAgeBasedReminders(nowUtc, stoppingToken);
                    dailyRuleAnchor = EasternTodayAt(_dailyRuleTimeEST).AddDays(1);
                }

                // Wait for the next poll interval, or exit if shutting down
                try { await Task.Delay(_pollInterval, stoppingToken); }
                catch (TaskCanceledException) { /* shutting down */ }
            }
        }

        /// <summary>
        /// Gets today's date at a specific time in Eastern Time, converted to UTC.
        /// Handles daylight saving time.
        /// </summary>
        private static DateTime EasternTodayAt(TimeSpan time)
        {
            var eastern = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var easternNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, eastern);
            var easternTodayAt = easternNow.Date.Add(time);
            return TimeZoneInfo.ConvertTimeToUtc(easternTodayAt, eastern);
        }

        // Finds and processes reminders that are due to run
        private async Task ProcessDueReminders(DateTime nowUtc, CancellationToken ct)
        {
            using var scope = _services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ThriveKidContext>();

            // Find reminders that are due and not completed
            var dues = await db.Reminders
                .Where(r => !r.IsCompleted && r.NextRunAt != null && r.NextRunAt <= nowUtc)
                .ToListAsync(ct);

            if (dues.Count == 0) return;

            foreach (var r in dues)
            {
                // Mark as run
                r.LastRunAt = nowUtc;

                // Calculate next run time based on repeat rule
                DateTime? next = r.RepeatRule switch
                {
                    RepeatRule.NONE    => null,                      // one-shot
                    RepeatRule.DAILY   => r.LastRunAt?.AddDays(1),
                    RepeatRule.WEEKLY  => r.LastRunAt?.AddDays(7),
                    RepeatRule.MONTHLY => r.LastRunAt?.AddMonths(1),
                    _ => null
                };

                if (r.RepeatRule == RepeatRule.NONE)
                {
                    // Mark one-shot reminders as completed
                    r.IsCompleted = true;
                    r.NextRunAt = null;
                }
                else
                {
                    // Prevent scheduling in the past
                    if (next != null && next <= nowUtc)
                        next = nowUtc.AddSeconds(1);

                    r.NextRunAt = next;
                }

                // Log for debugging
                Console.WriteLine(
                    $"[ReminderEngine] Ran ID={r.Id}, Title='{r.Title}', ChildId={r.ChildId}, " +
                    $"Repeat={r.RepeatRule}, LastRunAt={r.LastRunAt:O}, NextRunAt={(r.NextRunAt?.ToString("O") ?? "null")}");
            }

            await db.SaveChangesAsync(ct);
        }

        // Applies age-based reminder rules for each child, once per day
        private async Task ApplyAgeBasedReminders(DateTime nowUtc, CancellationToken ct)
        {
            using var scope = _services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ThriveKidContext>();

            var children = await db.Children.ToListAsync(ct);
            foreach (var c in children)
            {
                var months = MonthsBetween(c.DateOfBirth, nowUtc);
                var suggestions = AgeBasedReminderRules.GetForAgeMonths(months);

                foreach (var s in suggestions)
                {
                    // Skip if a similar reminder was auto-created in the last 7 days
                    var sevenDaysAgo = nowUtc.AddDays(-7);
                    var exists = await db.Reminders.AnyAsync(r =>
                        r.ChildId == c.Id &&
                        r.Source == ReminderSource.AutoAgeRule &&
                        r.Title == s.Title &&
                        r.DueAt >= sevenDaysAgo,
                        ct);

                    if (exists) continue;

                    db.Reminders.Add(new Reminder
                    {
                        ChildId = c.Id,
                        Title = s.Title,
                        Notes = s.Notes,
                        DueAt = nowUtc,          // first run now
                        RepeatRule = s.Repeat,
                        IsCompleted = false,
                        LastRunAt = null,
                        NextRunAt = nowUtc,      // fire on next tick
                        Source = ReminderSource.AutoAgeRule
                    });

                    Console.WriteLine(
                        $"[ReminderEngine] Auto-added age reminder for ChildId={c.Id} '{s.Title}' at {nowUtc:O}");
                }
            }

            await db.SaveChangesAsync(ct);
        }

        // Helper: calculates the number of months between two dates
        private static int MonthsBetween(DateTime dob, DateTime utcNow)
        {
            var a = new DateTime(dob.Year, dob.Month, 1);
            var b = new DateTime(utcNow.Year, utcNow.Month, 1);
            return ((b.Year - a.Year) * 12) + b.Month - a.Month;
        }
    }
}