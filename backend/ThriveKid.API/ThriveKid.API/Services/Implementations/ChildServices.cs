
using Microsoft.EntityFrameworkCore;
using ThriveKid.API.Models;
using ThriveKid.API.DTOs.Children;
using ThriveKid.API.Services.Interfaces;
using ThriveKid.API.Data;
using System.Linq;


namespace ThriveKid.API.Services.Implementations
{
    public class ChildServices : IChildService
    {
        // Service class that implements IChildService interface
        // This class contains the business logic for managing child records
        // It interacts with the database context to perform CRUD operations on the Children table.
        private readonly ThriveKidContext _context;
        public ChildServices(ThriveKidContext context)
        {
            _context = context;
        }
         public async Task<IEnumerable<ChildDto>> GetAllAsync()
        {
            var now = DateTime.UtcNow;
            return await _context.Children.AsNoTracking()
                .Select(c => new ChildDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    DateOfBirth = c.DateOfBirth,
                    Gender = c.Gender.ToString(),
                    AgeInMonths = Child.ComputeAgeInMonths(c.DateOfBirth, now)
                })
                .ToListAsync();
        }

        public async Task<ChildDto?> GetByIdAsync(int id)
        {
            var c = await _context.Children.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (c == null) return null;

            return new ChildDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName  = c.LastName,
                DateOfBirth = c.DateOfBirth,
                Gender = c.Gender.ToString(),
                AgeInMonths = Child.ComputeAgeInMonths(c.DateOfBirth, DateTime.UtcNow)
            };
        }

        public async Task<ChildDto> CreateAsync(CreateChildDto dto)
        {
            var e = new Child
            {
                FirstName = dto.FirstName.Trim(),
                LastName  = dto.LastName.Trim(),
                DateOfBirth = DateTime.SpecifyKind(dto.DateOfBirth, DateTimeKind.Utc),
                Gender = ParseGender(dto.Gender)
            };

            _context.Children.Add(e);
            await _context.SaveChangesAsync();
            return (await GetByIdAsync(e.Id))!;
        }

        public async Task<bool> UpdateAsync(int id, UpdateChildDto dto)
        {
            var e = await _context.Children.FindAsync(id);
            if (e == null) return false;

            e.FirstName = dto.FirstName.Trim();
            e.LastName  = dto.LastName.Trim();
            e.DateOfBirth = DateTime.SpecifyKind(dto.DateOfBirth, DateTimeKind.Utc);
            e.Gender = ParseGender(dto.Gender);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _context.Children.FindAsync(id);
            if (e == null) return false;

            _context.Children.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }

        private static Gender ParseGender(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return Gender.Unknown;
            var v = input.Trim().ToLowerInvariant();
            if (v is "f") return Gender.Female;
            if (v is "m") return Gender.Male;
            if (v is "u" or "x") return Gender.Unknown;
            return Enum.TryParse<Gender>(input, true, out var result) ? result : Gender.Unknown;
        }
    }
}