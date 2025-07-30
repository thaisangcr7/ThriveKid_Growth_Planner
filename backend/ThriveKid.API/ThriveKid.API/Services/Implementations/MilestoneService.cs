using Microsoft.EntityFrameworkCore;
using ThriveKid.API.Data;
using ThriveKid.API.DTOs.Milestones;
using ThriveKid.API.Services.Interfaces;

namespace ThriveKid.API.Services.Implementations
{
    public class MilestoneService : IMilestoneService
    {
        private readonly ThriveKidContext _context;

        public MilestoneService(ThriveKidContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MilestoneDto>> GetAllAsync()
        {
            return await _context.Milestones
                .Include(m => m.Child)
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

        public async Task<MilestoneDto?> GetByIdAsync(int id)
        {
            var m = await _context.Milestones.Include(m => m.Child).FirstOrDefaultAsync(m => m.Id == id);
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

            // Re-fetch to include child info
            return await GetByIdAsync(milestone.Id);
        }

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
