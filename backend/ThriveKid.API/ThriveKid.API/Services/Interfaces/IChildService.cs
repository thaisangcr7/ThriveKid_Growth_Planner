using ThriveKid.API.DTOs.Children;


namespace ThriveKid.API.Services.Interfaces
{
    public interface IChildService
    {
        Task<IEnumerable<ChildDto>> GetAllAsync();
        Task<ChildDto?> GetByIdAsync(int id);
        Task<ChildDto> CreateAsync(CreateChildDto dto);
        Task<bool> UpdateAsync(int id, UpdateChildDto dto);
        Task<bool> DeleteAsync(int id);
    }
}