using ThriveKid.API.DTOs.FeedingLogs;

namespace ThriveKid.API.Services.Interfaces
{
    public interface IFeedingLogService
    {
        Task<IEnumerable<FeedingLogDto>> GetAllAsync();
        Task<FeedingLogDto?> GetByIdAsync(int id);
        Task<FeedingLogDto> CreateAsync(CreateFeedingLogDto dto, int childId);
        Task<bool> UpdateAsync(int id, UpdateFeedingLogDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
