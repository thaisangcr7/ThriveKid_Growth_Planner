using FluentValidation;
using ThriveKid.API.DTOs.ToyRecommendations;

namespace ThriveKid.API.Validators.ToyRecommendations
{
    public class CreateToyRecommendationDtoValidator : AbstractValidator<CreateToyRecommendationDto>
    {
        public CreateToyRecommendationDtoValidator()
        {
            RuleFor(x => x.ToyName)
                .NotEmpty().WithMessage("Toy name is required.")
                .MaximumLength(100).WithMessage("Toy name must be 100 characters or less.");

            RuleFor(x => x.RecommendedAgeInMonths)
                .GreaterThanOrEqualTo(0).WithMessage("Recommended age must be 0 or more.");

            // Category is optional — no rule needed
        }
    }
}