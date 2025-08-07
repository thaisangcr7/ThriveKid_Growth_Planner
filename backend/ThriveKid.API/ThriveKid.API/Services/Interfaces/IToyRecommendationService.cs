using ThriveKid.API.DTOs.ToyRecommendations;

namespace ThriveKid.API.Services.Interfaces
{
    public interface IToyRecommendationService
    {
        Task<IEnumerable<ToyRecommendationDto>> GetAllToyRecommendationsAsync();
        Task<ToyRecommendationDto?> GetToyRecommendationByIdAsync(int id);
        Task<ToyRecommendationDto> CreateToyRecommendationAsync(CreateToyRecommendationDto dto);
        Task<bool> UpdateToyRecommendationAsync(int id, UpdateToyRecommendationDto dto);
        Task<bool> DeleteToyRecommendationAsync(int id);
    }
}