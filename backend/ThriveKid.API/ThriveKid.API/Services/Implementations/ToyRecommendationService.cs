using Microsoft.EntityFrameworkCore;                  // For async DB operations and Include
using ThriveKid.API.Data;                             // For ThriveKidContext (EF Core DB context)
using ThriveKid.API.DTOs.ToyRecommendations;          // For ToyRecommendationDto, CreateToyRecommendationDto, UpdateToyRecommendationDto
using ThriveKid.API.Models;                           // For ToyRecommendation entity
using ThriveKid.API.Services.Interfaces;              // For IToyRecommendationService interface

namespace ThriveKid.API.Services.Implementations
{
    // Implements business logic and data access for toy recommendations
    public class ToyRecommendationService : IToyRecommendationService
    {
        private readonly ThriveKidContext _context;   // EF Core DB context

        public ToyRecommendationService(ThriveKidContext context)
        {
            _context = context;
        }

        // Returns all toy recommendations, including child info, as DTOs
        public async Task<IEnumerable<ToyRecommendationDto>> GetAllToyRecommendationsAsync()
        {
            return await _context.ToyRecommendations
                .Include(t => t.Child) // Eager-load child info
                .Select(t => new ToyRecommendationDto
                {
                    Id = t.Id,
                    ToyName = t.ToyName,
                    RecommendedAgeInMonths = t.RecommendedAgeInMonths,
                    Category = t.Category,
                    ChildId = t.ChildId,
                    ChildName = t.Child.FirstName + " " + t.Child.LastName
                })
                .ToListAsync();
        }

        // Returns a single toy recommendation by ID, or null if not found
        public async Task<ToyRecommendationDto?> GetToyRecommendationByIdAsync(int id)
        {
            var toy = await _context.ToyRecommendations
                .Include(t => t.Child)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (toy == null)
                return null;

            return new ToyRecommendationDto
            {
                Id = toy.Id,
                ToyName = toy.ToyName,
                RecommendedAgeInMonths = toy.RecommendedAgeInMonths,
                Category = toy.Category,
                ChildId = toy.ChildId,
                ChildName = toy.Child?.FirstName + " " + toy.Child?.LastName
            };
        }

        // Creates a new toy recommendation and returns the DTO (with child info)
        public async Task<ToyRecommendationDto> CreateToyRecommendationAsync(CreateToyRecommendationDto dto)
        {
            var toy = new ToyRecommendation
            {
                ToyName = dto.ToyName,
                RecommendedAgeInMonths = dto.RecommendedAgeInMonths,
                Category = dto.Category,
                ChildId = dto.ChildId
            };

            _context.ToyRecommendations.Add(toy);
            await _context.SaveChangesAsync();

            // Load child info for response
            var child = await _context.Children.FindAsync(dto.ChildId);

            return new ToyRecommendationDto
            {
                Id = toy.Id,
                ToyName = toy.ToyName,
                RecommendedAgeInMonths = toy.RecommendedAgeInMonths,
                Category = toy.Category,
                ChildId = toy.ChildId,
                ChildName = child != null ? child.FirstName + " " + child.LastName : ""
            };
        }

        // Updates an existing toy recommendation; returns true if successful
        public async Task<bool> UpdateToyRecommendationAsync(int id, UpdateToyRecommendationDto dto)
        {
            var toy = await _context.ToyRecommendations.FindAsync(id);
            if (toy == null)
                return false;

            toy.ToyName = dto.ToyName;
            toy.RecommendedAgeInMonths = dto.RecommendedAgeInMonths;
            toy.Category = dto.Category;

            await _context.SaveChangesAsync();
            return true;
        }

        // Deletes a toy recommendation by ID; returns true if successful
        public async Task<bool> DeleteToyRecommendationAsync(int id)
        {
            var toy = await _context.ToyRecommendations.FindAsync(id);
            if (toy == null)
                return false;

            _context.ToyRecommendations.Remove(toy);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}