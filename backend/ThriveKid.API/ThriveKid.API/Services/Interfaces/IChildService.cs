using ThriveKid.API.Models;
using ThriveKid.API.DTOs.Children;

namespace ThriveKid.API.Services.Interfaces
{
    public interface IChildService
    {
        // Get all children
        Task<IEnumerable<Child>> GetAllAsync();

        // Get a child by its ID
        Task<Child?> GetByIdAsync(int id);

        // ✅ Create a new child from CreateChildDto
        Task<Child> CreateChildAsync(CreateChildDto createDto);

        // ✅ Update an existing child using UpdateChildDto
        Task<bool> UpdateAsync(int id, UpdateChildDto updateDto);

        // Delete a child by ID
        Task<bool> DeleteAsync(int id);
    }
}
