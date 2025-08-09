using Microsoft.EntityFrameworkCore;                  // For async DB operations and Include
using ThriveKid.API.Data;                             // For ThriveKidContext (EF Core DB context)
using ThriveKid.API.DTOs.LearningGoals;               // For LearningGoalDto, CreateLearningGoalDto, UpdateLearningGoalDto
using ThriveKid.API.Models;                           // For LearningGoal entity
using ThriveKid.API.Services.Interfaces;              // For ILearningGoalService interface

namespace ThriveKid.API.Services.Implementations
{
    // Implements business logic and data access for learning goals
    public class LearningGoalService : ILearningGoalService
    {
        private readonly ThriveKidContext _context;   // EF Core DB context

        public LearningGoalService(ThriveKidContext context)
        {
            _context = context;
        }

        // Returns all learning goals, including child info, as DTOs
        public async Task<IEnumerable<LearningGoalDto>> GetAllAsync()
        {
            return await _context.LearningGoals
                .Include(lg => lg.Child) // Eager-load child info
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

        // Returns a single learning goal by ID, or null if not found
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

        // Creates a new learning goal and returns the DTO (with child info)
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

        // Updates an existing learning goal; returns updated DTO or null if not found
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

        // Deletes a learning goal by ID; returns true if successful
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