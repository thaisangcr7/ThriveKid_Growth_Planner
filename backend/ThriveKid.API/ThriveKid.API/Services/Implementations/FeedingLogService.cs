using Microsoft.EntityFrameworkCore;
using ThriveKid.API.Data;
using ThriveKid.API.DTOs.FeedingLogs;
using ThriveKid.API.Models;
using ThriveKid.API.Services.Interfaces;

namespace ThriveKid.API.Services.Implementations
{
    public class FeedingLogService : IFeedingLogService
    {
        private readonly ThriveKidContext _context;

        public FeedingLogService(ThriveKidContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FeedingLogDto>> GetAllAsync()
        {
            return await _context.FeedingLogs
                .Include(f => f.Child)
                .Select(f => new FeedingLogDto
                {
                    Id = f.Id,
                    FeedingTime = f.FeedingTime,
                    MealType = f.MealType,
                    Notes = f.Notes,
                    ChildId = f.ChildId,
                    ChildName = f.Child.FirstName + " " + f.Child.LastName
                })
                .ToListAsync();
        }

        public async Task<FeedingLogDto?> GetByIdAsync(int id)
        {
            var f = await _context.FeedingLogs.Include(f => f.Child).FirstOrDefaultAsync(f => f.Id == id);
            if (f == null) return null;

            return new FeedingLogDto
            {
                Id = f.Id,
                FeedingTime = f.FeedingTime,
                MealType = f.MealType,
                Notes = f.Notes,
                ChildId = f.ChildId,
                ChildName = f.Child.FirstName + " " + f.Child.LastName
            };
        }

        public async Task<FeedingLogDto> CreateAsync(CreateFeedingLogDto dto, int childId)
        {
            var log = new FeedingLog
            {
                FeedingTime = dto.FeedingTime,
                MealType = dto.MealType,
                Notes = dto.Notes,
                ChildId = childId
            };

            _context.FeedingLogs.Add(log);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(log.Id); // includes child info
        }

        public async Task<bool> UpdateAsync(int id, UpdateFeedingLogDto dto)
        {
            var existing = await _context.FeedingLogs.FindAsync(id);
            if (existing == null) return false;

            existing.FeedingTime = dto.FeedingTime;
            existing.MealType = dto.MealType;
            existing.Notes = dto.Notes;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.FeedingLogs.FindAsync(id);
            if (existing == null) return false;

            _context.FeedingLogs.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
