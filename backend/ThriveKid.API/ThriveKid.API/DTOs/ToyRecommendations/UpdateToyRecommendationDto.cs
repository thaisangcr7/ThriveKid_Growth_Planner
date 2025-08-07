using System;

namespace ThriveKid.API.DTOs.ToyRecommendations
{
    public class UpdateToyRecommendationDto
    {
        public string ToyName { get; set; } = string.Empty;
        public int RecommendedAgeInMonths { get; set; }
        public string? Category { get; set; }

    }
}


