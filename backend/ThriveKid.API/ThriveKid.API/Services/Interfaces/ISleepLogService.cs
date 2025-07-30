using ThriveKid.API.DTOs.SleepLogs;

namespace ThriveKid.API.Services.Interfaces
{
    public interface ISleepLogService
    {
        Task<IEnumerable<SleepLogDto>> GetAllSleepLogsAsync();
        Task<SleepLogDto> GetSleepLogByIdAsync(int id);
        Task<SleepLogDto> CreateSleepLogAsync(CreateSleepLogDto sleepLogDto);
        Task<bool> UpdateSleepLogAsync(int id, UpdateSleepLogDto sleepLogDto);
        Task<bool> DeleteSleepLogAsync(int id);
    }
}
