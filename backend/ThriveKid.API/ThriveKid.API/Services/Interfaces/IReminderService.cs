using ThriveKid.API.DTOs.Reminders;

namespace ThriveKid.API.Services.Interfaces
{
    public interface IReminderService
    {
        Task<IEnumerable<ReminderDto>> GetAllAsync();
        Task<ReminderDto?> GetByIdAsync(int id);
        Task<ReminderDto> CreateAsync(CreateReminderDto dto);
        Task<bool> UpdateAsync(int id, UpdateReminderDto dto);
        Task<bool> DeleteAsync(int id);

        // Engine helpers
        Task<bool> SetCompletedAsync(int id, bool isCompleted);
        DateTime? ComputeNextRun(DateTime fromUtc, string repeatRule);
    }
}