using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ThriveKid.API.Data;
using ThriveKid.API.Models;
using ThriveKid.API.Services.Engines.AgeRules;

namespace ThriveKid.API.Services.Engines
{
    public class ReminderEngine : BackgroundService
    {
        private readonly IServiceProvider _services;

        // Poll every 60s
        private readonly TimeSpan _pollInterval = TimeSpan.FromSeconds(60);

        // Run age-based rules daily at 2:00 AM Eastern (handles EST/EDT automatically)
        private readonly TimeSpan _dailyRuleTimeEST = new(2, 0, 0);

        public ReminderEngine(IServiceProvider services) => _services = services;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var dailyRuleAnchor = EasternTodayAt(_dailyRuleTimeEST);

            while (!stoppingToken.IsCancellationRequested)
            {
                var nowUtc = DateTime.UtcNow;

                await ProcessDueReminders(nowUtc, stoppingToken);

                // Apply age-based rules once per day after the Eastern anchor time
                if (nowUtc >= dailyRuleAnchor)
                {
                    await ApplyAgeBasedReminders(nowUtc, stoppingToken);
                    dailyRuleAnchor = EasternTodayAt(_dailyRuleTimeEST).AddDays(1);
                }

                try { await Task.Delay(_pollInterval, stoppingToken); }
                catch (TaskCanceledException) { /* shutting down */ }
            }
        }

        /// <summary>
        /// Convert "today at {time} in Eastern" to UTC, handling DST.
        /// </summary>
        private static DateTime EasternTodayAt(TimeSpan time)
        {
            var eastern = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var easternNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, eastern);
            var easternTodayAt = easternNow.Date.Add(time);                 // today 02:00 in Eastern
            return TimeZoneInfo.ConvertTimeToUtc(easternTodayAt, eastern);  // convert to UTC anchor
        }

        private async Task ProcessDueReminders(DateTime nowUtc, CancellationToken ct)
        {
            using var scope = _services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ThriveKidContext>();

            // Find due + not completed
            var dues = await db.Reminders
                .Where(r => !r.IsCompleted && r.NextRunAt != null && r.NextRunAt <= nowUtc)
                .ToListAsync(ct);

            if (dues.Count == 0) return;

            foreach (var r in dues)
            {
                // mark this execution
                r.LastRunAt = nowUtc;

                // compute next occurrence
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
                    // ✅ complete one-shots
                    r.IsCompleted = true;
                    r.NextRunAt = null;
                }
                else
                {
                    // drift guard: never schedule in the past
                    if (next != null && next <= nowUtc)
                        next = nowUtc.AddSeconds(1);

                    r.NextRunAt = next;
                }

                // (optional) console log for visibility during testing
                Console.WriteLine(
                    $"[ReminderEngine] Ran ID={r.Id}, Title='{r.Title}', ChildId={r.ChildId}, " +
                    $"Repeat={r.RepeatRule}, LastRunAt={r.LastRunAt:O}, NextRunAt={(r.NextRunAt?.ToString("O") ?? "null")}");
            }

            await db.SaveChangesAsync(ct);
        }

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
                    // de-dupe: skip if same title auto-created in last 7 days
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

        private static int MonthsBetween(DateTime dob, DateTime utcNow)
        {
            var a = new DateTime(dob.Year, dob.Month, 1);
            var b = new DateTime(utcNow.Year, utcNow.Month, 1);
            return ((b.Year - a.Year) * 12) + b.Month - a.Month;
        }
    }
}