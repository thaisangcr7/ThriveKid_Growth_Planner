using Microsoft.EntityFrameworkCore;              // For async DB operations and Include
using ThriveKid.API.Data;                         // For ThriveKidContext (EF Core DB context)
using ThriveKid.API.DTOs.SleepLogs;               // For SleepLogDto, CreateSleepLogDto, UpdateSleepLogDto
using ThriveKid.API.Models;                       // For SleepLog entity
using ThriveKid.API.Services.Interfaces;          // For ISleepLogService interface

namespace ThriveKid.API.Services
{
    // Implements business logic and data access for sleep logs
    public class SleepLogService : ISleepLogService
    {
        private readonly ThriveKidContext _context; // EF Core DB context

        public SleepLogService(ThriveKidContext context)
        {
            _context = context;
        }

        // Get all sleep logs and include child's full name
        public async Task<IEnumerable<SleepLogDto>> GetAllSleepLogsAsync()
        {
            var sleepLogs = await _context.SleepLogs.Include(sl => sl.Child).ToListAsync();

            // Project each SleepLog entity to a DTO, including child info and sleep duration
            return sleepLogs.Select(sl => new SleepLogDto
            {
                Id = sl.Id,
                StartTime = sl.StartTime,
                EndTime = sl.EndTime,
                Notes = sl.Notes ?? "",
                ChildId = sl.ChildId,
                ChildName = sl.Child != null ? sl.Child.FirstName + " " + sl.Child.LastName : "Unknown",
                SleepDurationHours = (sl.EndTime - sl.StartTime).TotalHours
            });
        }

        // Get a single sleep log by ID
        public async Task<SleepLogDto?> GetSleepLogByIdAsync(int id)
        {
            var sl = await _context.SleepLogs.Include(sl => sl.Child).FirstOrDefaultAsync(sl => sl.Id == id);
            if (sl == null) return null;

            return new SleepLogDto
            {
                Id = sl.Id,
                StartTime = sl.StartTime,
                EndTime = sl.EndTime,
                Notes = sl.Notes ?? "",
                ChildId = sl.ChildId,
                ChildName = sl.Child != null ? sl.Child.FirstName + " " + sl.Child.LastName : "Unknown",
                SleepDurationHours = (sl.EndTime - sl.StartTime).TotalHours
            };
        }

        // Create a new sleep log
        public async Task<SleepLogDto> CreateSleepLogAsync(CreateSleepLogDto dto)
        {
            var sleepLog = new SleepLog
            {
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Notes = dto.Notes,
                ChildId = dto.ChildId
            };

            _context.SleepLogs.Add(sleepLog);
            await _context.SaveChangesAsync();

            // Load child info to return full DTO
            var child = await _context.Children.FindAsync(dto.ChildId);

            return new SleepLogDto
            {
                Id = sleepLog.Id,
                StartTime = sleepLog.StartTime,
                EndTime = sleepLog.EndTime,
                Notes = sleepLog.Notes ?? "",
                ChildId = sleepLog.ChildId,
                ChildName = child.FirstName + " " + child.LastName,
                SleepDurationHours = (sleepLog.EndTime - sleepLog.StartTime).TotalHours
            };
        }

        // Update existing sleep logs
        public async Task<bool> UpdateSleepLogAsync(int id, UpdateSleepLogDto dto)
        {
            var sleepLog = await _context.SleepLogs.FindAsync(id);
            if (sleepLog == null) return false;

            sleepLog.StartTime = dto.StartTime;
            sleepLog.EndTime = dto.EndTime;
            sleepLog.Notes = dto.Notes;

            await _context.SaveChangesAsync();
            return true;
        }

        // Delete sleep log
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