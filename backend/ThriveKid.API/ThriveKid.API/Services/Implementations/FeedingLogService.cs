using Microsoft.EntityFrameworkCore;                // For async DB operations and Include
using ThriveKid.API.Data;                           // For ThriveKidContext (EF Core DB context)
using ThriveKid.API.DTOs.FeedingLogs;               // For FeedingLogDto, CreateFeedingLogDto, UpdateFeedingLogDto
using ThriveKid.API.Models;                         // For FeedingLog entity
using ThriveKid.API.Services.Interfaces;            // For IFeedingLogService interface

namespace ThriveKid.API.Services.Implementations
{
    // Implements feeding log business logic and data access
    public class FeedingLogService : IFeedingLogService
    {
        private readonly ThriveKidContext _context; // EF Core DB context

        public FeedingLogService(ThriveKidContext context)
        {
            _context = context;
        }

        // Returns all feeding logs, including child info, as DTOs
        public async Task<IEnumerable<FeedingLogDto>> GetAllAsync()
        {
            return await _context.FeedingLogs
                .Include(f => f.Child) // Eager-load child info
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

        // Returns a single feeding log by ID, or null if not found
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

        // Creates a new feeding log for a child and returns the DTO (with child info)
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

        // Updates an existing feeding log; returns true if successful
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

        // Deletes a feeding log by ID; returns true if successful
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