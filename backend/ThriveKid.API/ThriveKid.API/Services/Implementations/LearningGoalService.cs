using Microsoft.EntityFrameworkCore;
using ThriveKid.API.Data;
using ThriveKid.API.DTOs.LearningGoals;
using ThriveKid.API.Models;
using ThriveKid.API.Services.Interfaces;

namespace ThriveKid.API.Services.Implementations
{
    public class LearningGoalService : ILearningGoalService
    {
        private readonly ThriveKidContext _context;

        public LearningGoalService(ThriveKidContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LearningGoalDto>> GetAllAsync()
        {
            return await _context.LearningGoals
                .Include(lg => lg.Child)
                .Select(lg => new LearningGoalDto
                {
                    Id = lg.Id,
                    Title = lg.Title,
                    IsCompleted = lg.IsCompleted,
                    CompletedDate = lg.CompletedDate,
                    ChildId = lg.ChildId,
                    ChildName = lg.Child.FirstName + " " + lg.Child.LastName
                })
                .ToListAsync();
        }

        public async Task<LearningGoalDto?> GetByIdAsync(int id)
        {
            var lg = await _context.LearningGoals
                .Include(l => l.Child)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lg == null) return null;

            return new LearningGoalDto
            {
                Id = lg.Id,
                Title = lg.Title,
                IsCompleted = lg.IsCompleted,
                CompletedDate = lg.CompletedDate,
                ChildId = lg.ChildId,
                ChildName = lg.Child.FirstName + " " + lg.Child.LastName
            };
        }

        public async Task<LearningGoalDto> CreateAsync(CreateLearningGoalDto dto)
        {
            var lg = new LearningGoal
            {
                Title = dto.Title,
                ChildId = dto.ChildId
            };

            _context.LearningGoals.Add(lg);
            await _context.SaveChangesAsync();

            // load child for name
            var child = await _context.Children.FindAsync(dto.ChildId);
            return new LearningGoalDto
            {
                Id = lg.Id,
                Title = lg.Title,
                IsCompleted = lg.IsCompleted,
                CompletedDate = lg.CompletedDate,
                ChildId = lg.ChildId,
                ChildName = child != null 
                    ? child.FirstName + " " + child.LastName 
                    : string.Empty
            };
        }

        public async Task<LearningGoalDto?> UpdateAsync(int id, UpdateLearningGoalDto dto)
        {
            var lg = await _context.LearningGoals.FindAsync(id);
            if (lg == null) return null;

            lg.Title = dto.Title;
            lg.IsCompleted = dto.IsCompleted;
            lg.CompletedDate = dto.CompletedDate;

            await _context.SaveChangesAsync();

            // reload child
            var child = await _context.Children.FindAsync(lg.ChildId);
            return new LearningGoalDto
            {
                Id = lg.Id,
                Title = lg.Title,
                IsCompleted = lg.IsCompleted,
                CompletedDate = lg.CompletedDate,
                ChildId = lg.ChildId,
                ChildName = child != null 
                    ? child.FirstName + " " + child.LastName 
                    : string.Empty
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var lg = await _context.LearningGoals.FindAsync(id);
            if (lg == null) return false;

            _context.LearningGoals.Remove(lg);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}