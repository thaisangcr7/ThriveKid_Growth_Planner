
using Microsoft.EntityFrameworkCore;
using ThriveKid.API.Models;

namespace ThriveKid.API.Services
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

        // Create a new child record in the Children table.
        public async Task<Child> CreateAsync(Child child)
        {
            _context.Children.Add(child);
            await _context.SaveChangesAsync();
            return child;
        }

        // Update an existing child record by primary key (id).
        public async Task<bool> UpdateAsync(int id, Child child)
        {
            var existing = await _context.Children.FindAsync(id);
            if (existing == null) return false;

            existing.FirstName = child.FirstName;
            existing.LastName = child.LastName;
            existing.DateOfBirth = child.DateOfBirth;
            existing.Gender = child.Gender;
            existing.AgeInMonths = child.AgeInMonths;

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