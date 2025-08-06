using ThriveKid.API.DTOs.LearningGoals;

namespace ThriveKid.API.Services.Interfaces
{
    public interface ILearningGoalService
    {
        Task<IEnumerable<LearningGoalDto>> GetAllAsync();
        Task<LearningGoalDto?> GetByIdAsync(int id);
        Task<LearningGoalDto> CreateAsync(CreateLearningGoalDto dto);
        Task<LearningGoalDto?> UpdateAsync(int id, UpdateLearningGoalDto dto);
        Task<bool> DeleteAsync(int id);
    }
}