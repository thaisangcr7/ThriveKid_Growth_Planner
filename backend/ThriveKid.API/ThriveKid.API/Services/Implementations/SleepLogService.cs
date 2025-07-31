using Microsoft.EntityFrameworkCore;
using ThriveKid.API.Data;
using ThriveKid.API.DTOs.SleepLogs;
using ThriveKid.API.Models;
using ThriveKid.API.Services.Interfaces;

namespace ThriveKid.API.Services.Implementations
{
    public class SleepLogService : ISleepLogService
    {
        private readonly ThriveKidContext _context;

        public SleepLogService(ThriveKidContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SleepLogDto>> GetAllSleepLogsAsync()
        {
            return await _context.SleepLogs
                .Include(sl => sl.Child)
                .Select(sl => new SleepLogDto
                {
                    Id = sl.Id,
                    StartTime = sl.SleepStart,
                    EndTime = sl.SleepEnd,
                    Notes = sl.Notes,
                    ChildId = sl.ChildId,
                    ChildName = sl.Child.FirstName + " " + sl.Child.LastName
                })
                .ToListAsync();
        }

        public async Task<SleepLogDto?> GetSleepLogByIdAsync(int id)
        {
            var sl = await _context.SleepLogs
                .Include(sl => sl.Child)
                .FirstOrDefaultAsync(sl => sl.Id == id);

            if (sl == null) return null;

            return new SleepLogDto
            {
                Id = sl.Id,
                StartTime = sl.SleepStart,
                EndTime = sl.SleepEnd,
                Notes = sl.Notes,
                ChildId = sl.ChildId,
                ChildName = sl.Child.FirstName + " " + sl.Child.LastName
            };
        }

        public async Task<SleepLogDto> CreateSleepLogAsync(CreateSleepLogDto dto)
        {
            var sleepLog = new SleepLog
            {
                SleepStart = dto.StartTime,
                SleepEnd = dto.EndTime,
                Notes = dto.Notes,
                ChildId = dto.ChildId
            };

            _context.SleepLogs.Add(sleepLog);
            await _context.SaveChangesAsync();

            // Load child name after save
            var child = await _context.Children.FindAsync(dto.ChildId);

            return new SleepLogDto
            {
                Id = sleepLog.Id,
                StartTime = sleepLog.SleepStart,
                EndTime = sleepLog.SleepEnd,
                Notes = sleepLog.Notes,
                ChildId = sleepLog.ChildId,
                ChildName = child != null ? child.FirstName + " " + child.LastName : ""
            };
        }

        public async Task<bool> UpdateSleepLogAsync(int id, UpdateSleepLogDto dto)
        {
            var sleepLog = await _context.SleepLogs.FindAsync(id);
            if (sleepLog == null) return false;

            sleepLog.SleepStart = dto.StartTime;
            sleepLog.SleepEnd = dto.EndTime;
            sleepLog.Notes = dto.Notes;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSleepLogAsync(int id)
        {
            var sleepLog = await _context.SleepLogs.FindAsync(id);
            if (sleepLog == null) return false;

            _context.SleepLogs.Remove(sleepLog);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
