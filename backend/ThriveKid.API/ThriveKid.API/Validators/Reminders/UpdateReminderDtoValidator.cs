using FluentValidation;
using ThriveKid.API.DTOs.Reminders;

namespace ThriveKid.API.Validators.Reminders
{
    public class UpdateReminderDtoValidator : AbstractValidator<UpdateReminderDto>
    {
        public UpdateReminderDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(120);
            RuleFor(x => x.DueAt).NotEmpty();
            RuleFor(x => x.RepeatRule)
                .Must(v => new[] { "NONE", "DAILY", "WEEKLY", "MONTHLY" }.Contains(v.ToUpper()))
                .WithMessage("RepeatRule must be NONE, DAILY, WEEKLY, or MONTHLY.");
        }
    }
}