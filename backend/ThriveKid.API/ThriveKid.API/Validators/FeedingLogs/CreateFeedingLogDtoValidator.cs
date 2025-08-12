using FluentValidation;
using ThriveKid.API.DTOs.FeedingLogs;

namespace ThriveKid.API.Validators.FeedingLogs
{
    public class CreateFeedingLogDtoValidator : AbstractValidator<CreateFeedingLogDto>
    {
        private static readonly HashSet<string> Allowed = new(StringComparer.OrdinalIgnoreCase)
        { "Breastmilk", "Formula", "Puree", "Solid", "Snack", "Water" };

        public CreateFeedingLogDtoValidator()
        {
            RuleFor(x => x.FeedingTime)
                .NotEmpty().WithMessage("FeedingTime is required.")
                .LessThanOrEqualTo(DateTime.UtcNow.AddMinutes(1))
                .WithMessage("FeedingTime cannot be in the future.");

            RuleFor(x => x.MealType)
                .NotEmpty().WithMessage("MealType is required.")
                .Must(v => Allowed.Contains(v ?? string.Empty))
                .WithMessage($"MealType must be one of: {string.Join(", ", Allowed)}.");

            RuleFor(x => x.Notes)
                .MaximumLength(500).When(x => !string.IsNullOrWhiteSpace(x.Notes))
                .WithMessage("Notes must be 500 characters or fewer.");
        }
    }
}  