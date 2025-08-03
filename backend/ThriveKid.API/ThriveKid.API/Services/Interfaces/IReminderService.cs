using ThriveKid.API.DTOs.Reminders;

namespace ThriveKid.API.Services.Interfaces;

public interface IReminderService
{
    Task<List<ReminderDto>> GetAllRemindersAsync();
    Task<ReminderDto?> GetReminderByIdAsync(int id);
    Task<ReminderDto> CreateReminderAsync(CreateReminderDto dto);
    Task<ReminderDto?> UpdateReminderAsync(int id, UpdateReminderDto dto);
    Task<bool> DeleteReminderAsync(int id);
}
