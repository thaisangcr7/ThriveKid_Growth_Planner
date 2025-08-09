using Microsoft.EntityFrameworkCore;           // For async DB operations and Include
using ThriveKid.API.Data;                      // For ThriveKidContext (EF Core DB context)
using ThriveKid.API.DTOs.Milestones;           // For MilestoneDto, CreateMilestoneDto, UpdateMilestoneDto
using ThriveKid.API.Services.Interfaces;       // For IMilestoneService interface

namespace ThriveKid.API.Services.Implementations
{
    // Implements business logic and data access for milestones
    public class MilestoneService : IMilestoneService
    {
        private readonly ThriveKidContext _context; // EF Core DB context

        public MilestoneService(ThriveKidContext context)
        {
            _context = context;
        }

        // Returns all milestones, including child info, as DTOs
        public async Task<IEnumerable<MilestoneDto>> GetAllAsync()
        {
            return await _context.Milestones
                .Include(m => m.Child) // Eager-load child info for each milestone
                .Select(m => new MilestoneDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Notes = m.Notes,
                    AchievedDate = m.AchievedDate,
                    ChildId = m.ChildId,
                    ChildName = m.Child.FirstName + " " + m.Child.LastName
                })
                .ToListAsync();
        }

        // Returns a single milestone by ID, or null if not found
        public async Task<MilestoneDto?> GetByIdAsync(int id)
        {
            var m = await _context.Milestones
                .Include(m => m.Child)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (m == null) return null;

            return new MilestoneDto
            {
                Id = m.Id,
                Title = m.Title,
                Notes = m.Notes,
                AchievedDate = m.AchievedDate,
                ChildId = m.ChildId,
                ChildName = m.Child.FirstName + " " + m.Child.LastName
            };
        }

        // Creates a new milestone for a child and returns the DTO (with child info)
        public async Task<MilestoneDto> CreateAsync(CreateMilestoneDto dto, int childId)
        {
            var milestone = new Milestone
            {
                Title = dto.Title,
                Notes = dto.Notes,
                AchievedDate = dto.AchievedDate,
                ChildId = childId
            };

            _context.Milestones.Add(milestone);
            await _context.SaveChangesAsync();

            // Re-fetch to include child info in the DTO
            return await GetByIdAsync(milestone.Id);
        }

        // Updates an existing milestone; returns true if successful
        public async Task<bool> UpdateAsync(int id, UpdateMilestoneDto dto)
        {
            var existing = await _context.Milestones.FindAsync(id);
            if (existing == null) return false;

            existing.Title = dto.Title;
            existing.Notes = dto.Notes;
            existing.AchievedDate = dto.AchievedDate;

            await _context.SaveChangesAsync();
            return true;
        }

        // Deletes a milestone by ID; returns true if successful
        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Milestones.FindAsync(id);
            if (existing == null) return false;

            _context.Milestones.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}