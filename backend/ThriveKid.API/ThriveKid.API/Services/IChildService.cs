using ThriveKid.API.Models;

namespace ThriveKid.API.Services
{
    // Interface for child-related service operations
    public interface IChildService
    {
        // Get all children
        Task<IEnumerable<Child>> GetAllAsync();

        // Get a child by its ID
        Task<Child?> GetByIdAsync(int id);

        // Create a new child
        Task<Child> CreateAsync(Child child);

        // Update an existing child by ID
        Task<bool> UpdateAsync(int id, Child updatedChild);

        // Delete a child by ID
        Task<bool> DeleteAsync(int id);
    }
}
