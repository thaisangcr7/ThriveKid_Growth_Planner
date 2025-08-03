using FluentValidation;
using ThriveKid.API.DTOs.Reminders;

namespace ThriveKid.API.Validators.Reminders;

public class UpdateReminderDtoValidator : AbstractValidator<UpdateReminderDto>
{
    public UpdateReminderDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must be under 100 characters.");

        RuleFor(x => x.ReminderTime)
            .GreaterThan(DateTime.Now).WithMessage("Reminder time must be in the future.");
    }
}
