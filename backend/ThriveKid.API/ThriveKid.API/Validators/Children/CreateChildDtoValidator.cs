using FluentValidation;
using ThriveKid.API.DTOs.Children;

namespace ThriveKid.API.Validators.Children
{
    public class CreateChildDtoValidator : AbstractValidator<CreateChildDto>
    {
        public CreateChildDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(80).WithMessage("First name must be 80 characters or fewer.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(80).WithMessage("Last name must be 80 characters or fewer.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .LessThan(DateTime.UtcNow.Date.AddDays(1)).WithMessage("Date of birth cannot be in the future.")
                .GreaterThan(DateTime.UtcNow.Date.AddYears(-18)).WithMessage("Child must be younger than 18 years old.");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender is required. Use Unknown, Female, Male, or Other.")
                .Must(BeValidGender).WithMessage("Gender must be one of: Unknown, Female, Male, Other (case-insensitive).");
        }

        public static bool BeValidGender(string? g)
        {
            if (string.IsNullOrWhiteSpace(g)) return false;
            var v = g.Trim().ToLowerInvariant();
            return v is "unknown" or "female" or "male" or "other"
                // friendly shortcuts
                or "f" or "m" or "u" or "x";
        }
    }
}