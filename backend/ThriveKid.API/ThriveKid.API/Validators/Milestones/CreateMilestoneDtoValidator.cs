using FluentValidation;
using ThriveKid.API.DTOs.Milestones;

namespace ThriveKid.API.Validators.Milestones
{
    public class CreateMilestoneDtoValidator : AbstractValidator<CreateMilestoneDto>
    {
        public CreateMilestoneDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(120).WithMessage("Title must be 120 characters or fewer.");

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Notes must be 500 characters or fewer.")
                .When(x => !string.IsNullOrWhiteSpace(x.Notes));

            RuleFor(x => x.AchievedDate)
                .NotEmpty().WithMessage("Achieved date is required.")
                .Must(d => d.Date <= DateTime.UtcNow.Date)
                .WithMessage("Achieved date cannot be in the future.");
        }
    }
}