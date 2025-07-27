using ThriveKid.API.DTOs.Milestones;
using ThriveKid.API.Models;

namespace ThriveKid.API.Services.Interfaces
{
    public interface IMilestoneService
    {
        Task<IEnumerable<MilestoneDto>> GetAllAsync();
        Task<MilestoneDto?> GetByIdAsync(int id);
        Task<MilestoneDto> CreateAsync(CreateMilestoneDto dto, int childId);
        Task<bool> UpdateAsync(int id, UpdateMilestoneDto dto);
        Task<bool> DeleteAsync(int id);

    }
}

