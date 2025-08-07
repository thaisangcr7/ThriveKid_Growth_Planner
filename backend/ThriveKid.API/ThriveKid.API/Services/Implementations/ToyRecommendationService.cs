using Microsoft.EntityFrameworkCore;
using ThriveKid.API.Data;
using ThriveKid.API.DTOs.ToyRecommendations;
using ThriveKid.API.Models;
using ThriveKid.API.Services.Interfaces;

namespace ThriveKid.API.Services.Implementations
{
    public class ToyRecommendationService : IToyRecommendationService
    {
        private readonly ThriveKidContext _context;

        public ToyRecommendationService(ThriveKidContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ToyRecommendationDto>> GetAllToyRecommendationsAsync()
        {
            return await _context.ToyRecommendations
                .Include(t => t.Child)
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