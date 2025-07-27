using Microsoft.EntityFrameworkCore;
using ThriveKid.API.Models;
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

        public async Task<IEnumerable<Milestone>> GetAllAsync()
        {
            return await _context.Milestones.ToListAsync();
        }

        public async Task<Milestone?> GetByIdAsync(int id)
        {
            return await _context.Milestones.FindAsync(id);
        }

        public async Task<Milestone> CreateAsync(Milestone milestone)
        {
            _context.Milestones.Add(milestone);
            await _context.SaveChangesAsync();
            return milestone;
        }

        public async Task<bool> UpdateAsync(int id, Milestone updatedMilestone)
        {
            var existing = await _context.Milestones.FindAsync(id);
            if (existing == null) return false;

            existing.Description = updatedMilestone.Description;
            existing.AchievedDate = updatedMilestone.AchievedDate;
            existing.ChildId = updatedMilestone.ChildId;

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

/* 
 What this class does (line by line):
Method	What it does
GetAllAsync	Fetches all milestones from the database
GetByIdAsync(id)	Gets one milestone by ID
CreateAsync(milestone)	Adds a new milestone to the DB
UpdateAsync(id, updatedMilestone)	Updates an existing milestone if it exists
DeleteAsync(id)	Deletes the milestone if found
*/