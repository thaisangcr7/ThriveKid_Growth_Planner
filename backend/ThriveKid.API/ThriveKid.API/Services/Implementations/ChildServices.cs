
using Microsoft.EntityFrameworkCore;
using ThriveKid.API.Models;
using ThriveKid.API.DTOs.Children;
using ThriveKid.API.Services.Interfaces;

namespace ThriveKid.API.Services.Implementations
{
    // Service class that implements IChildService interface
    // This class contains the business logic for managing child records
    // It interacts with the database context to perform CRUD operations on the Children table.

    public class ChildService : IChildService
    {
        private readonly ThriveKidContext _context;

        public ChildService(ThriveKidContext context)
        {
            _context = context; // Inject DB context so we can access the Children table
        }

        // Get all rows from the Children table.
        public async Task<IEnumerable<Child>> GetAllAsync()
        {
            return await _context.Children.ToListAsync();
        }

        // Find one child by primary key (id).
        public async Task<Child?> GetByIdAsync(int id)
        {
            return await _context.Children.FindAsync(id);
        }

        // Create a new child record using CreateChildDto.
        public async Task<Child> CreateChildAsync(CreateChildDto createDto)
        {
            var child = new Child
            {
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                DateOfBirth = createDto.DateOfBirth,
                Gender = createDto.Gender,
                AgeInMonths = createDto.AgeInMonths
            };

            _context.Children.Add(child);
            await _context.SaveChangesAsync();
            return child;
        }

        // Update an existing child record by primary key (id) using UpdateChildDto.
        public async Task<bool> UpdateAsync(int id, UpdateChildDto updateDto)
{
    var existing = await _context.Children.FindAsync(id);
    if (existing == null) return false;

    existing.FirstName = updateDto.FirstName;
    existing.LastName = updateDto.LastName;
    existing.DateOfBirth = updateDto.DateOfBirth ?? existing.DateOfBirth;
    existing.Gender = updateDto.Gender;
    existing.AgeInMonths = updateDto.AgeInMonths ?? existing.AgeInMonths;

    await _context.SaveChangesAsync();
    return true;
}


        // Delete a child record by primary key (id).
        public async Task<bool> DeleteAsync(int id)
        {
            var child = await _context.Children.FindAsync(id);
            if (child == null) return false;

            _context.Children.Remove(child);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
