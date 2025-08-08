using Microsoft.EntityFrameworkCore;
using ThriveKid.API.Data;
using ThriveKid.API.DTOs.Reminders;
using ThriveKid.API.Models;
using ThriveKid.API.Services.Interfaces;

namespace ThriveKid.API.Services.Implementations
{
    public class ReminderService : IReminderService
    {
        private readonly ThriveKidContext _context;

        public ReminderService(ThriveKidContext context) => _context = context;

        public async Task<IEnumerable<ReminderDto>> GetAllAsync()
        {
            return await _context.Reminders
                .Include(r => r.Child)
                .Select(r => new ReminderDto
                {
                    Id = r.Id,
                    ChildId = r.ChildId,
                    ChildName = r.Child != null ? r.Child.FirstName + " " + r.Child.LastName : "",
                    Title = r.Title,
                    Notes = r.Notes,
                    DueAt = r.DueAt,
                    RepeatRule = r.RepeatRule.ToString(),
                    IsCompleted = r.IsCompleted,
                    LastRunAt = r.LastRunAt,
                    NextRunAt = r.NextRunAt,
                    Source = r.Source.ToString()
                })
                .ToListAsync();
        }

        public async Task<ReminderDto?> GetByIdAsync(int id)
        {
            var r = await _context.Reminders
                .Include(x => x.Child)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (r == null) return null;

            return new ReminderDto
            {
                Id = r.Id,
                ChildId = r.ChildId,
                ChildName = r.Child != null ? r.Child.FirstName + " " + r.Child.LastName : "",
                Title = r.Title,
                Notes = r.Notes,
                DueAt = r.DueAt,
                RepeatRule = r.RepeatRule.ToString(),
                IsCompleted = r.IsCompleted,
                LastRunAt = r.LastRunAt,
                NextRunAt = r.NextRunAt,
                Source = r.Source.ToString()
            };
        }

        public async Task<ReminderDto> CreateAsync(CreateReminderDto dto)
        {
            var rule = ParseRule(dto.RepeatRule);

            var dueUtc = DateTime.SpecifyKind(dto.DueAt, DateTimeKind.Utc);

            var entity = new Reminder
            {
                ChildId = dto.ChildId,
                Title = dto.Title,
                Notes = dto.Notes,
                DueAt = dueUtc,
                RepeatRule = rule,

                // engine bookkeeping
                IsCompleted = false,
                LastRunAt = null,

                // 👇 First run should be at DueAt so the engine will pick it up
                NextRunAt = dueUtc,

                Source = ReminderSource.Manual
            };

            _context.Reminders.Add(entity);
            await _context.SaveChangesAsync();

            // return fully projected DTO
            return (await GetByIdAsync(entity.Id))!;
        }

        public async Task<bool> UpdateAsync(int id, UpdateReminderDto dto)
        {
            var r = await _context.Reminders.FindAsync(id);
            if (r == null) return false;

            r.Title = dto.Title;
            r.Notes = dto.Notes;
            r.DueAt = DateTime.SpecifyKind(dto.DueAt, DateTimeKind.Utc);
            r.RepeatRule = ParseRule(dto.RepeatRule);
            r.IsCompleted = dto.IsCompleted;

            // 👇 When user changes due/recurrence, schedule next run at new DueAt
            r.NextRunAt = r.DueAt;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var r = await _context.Reminders.FindAsync(id);
            if (r == null) return false;

            _context.Reminders.Remove(r);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetCompletedAsync(int id, bool isCompleted)
        {
            var r = await _context.Reminders.FindAsync(id);
            if (r == null) return false;

            r.IsCompleted = isCompleted;
            await _context.SaveChangesAsync();
            return true;
        }

        // Still useful for tests or if you ever want precompute behavior
        public DateTime? ComputeNextRun(DateTime fromUtc, string repeatRule)
        {
            var utc = DateTime.SpecifyKind(fromUtc, DateTimeKind.Utc);
            switch (ParseRule(repeatRule))
            {
                case RepeatRule.NONE:    return utc;            // first fire at DueAt
                case RepeatRule.DAILY:   return utc.AddDays(1);
                case RepeatRule.WEEKLY:  return utc.AddDays(7);
                case RepeatRule.MONTHLY: return utc.AddMonths(1);
                default: return utc;
            }
        }

        private static RepeatRule ParseRule(string rule)
            => Enum.TryParse<RepeatRule>(rule, true, out var rr) ? rr : RepeatRule.NONE;
    }
}