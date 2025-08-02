using FluentValidation;
using ThriveKid.API.DTOs.SleepLogs;

namespace ThriveKid.API.Validators.SleepLogs
{
    public class CreateSleepLogDtoValidator : AbstractValidator<CreateSleepLogDto>
    {
        public CreateSleepLogDtoValidator()
        {
            RuleFor(x => x.ChildId)
                .GreaterThan(0).WithMessage("ChildId must be a positive integer.");

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("StartTime is required.");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("EndTime is required.")
                .GreaterThan(x => x.StartTime).WithMessage("EndTime must be after StartTime.");

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Notes must not exceed 500 characters.");
        }
    }
}
