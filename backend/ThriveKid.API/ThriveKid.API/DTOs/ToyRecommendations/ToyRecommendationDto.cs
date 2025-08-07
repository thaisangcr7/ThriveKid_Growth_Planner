using System;

namespace ThriveKid.API.DTOs.ToyRecommendations
{
    public class ToyRecommendationDto
    {
        public int Id { get; set; }
        public string ToyName { get; set; } = string.Empty;
        public int RecommendedAgeInMonths { get; set; }
        public string? Category { get; set; }
        public int ChildId { get; set; }
        public string ChildName { get; set; } = string.Empty;
    }
}
