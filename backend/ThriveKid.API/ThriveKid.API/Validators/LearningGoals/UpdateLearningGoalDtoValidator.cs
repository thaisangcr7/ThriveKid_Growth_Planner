// Validators/LearningGoals/UpdateLearningGoalDtoValidator.cs
using System;
using FluentValidation;
using ThriveKid.API.DTOs.LearningGoals;

namespace ThriveKid.API.Validators.LearningGoals
{
    public class UpdateLearningGoalDtoValidator : AbstractValidator<UpdateLearningGoalDto>
    {
        public UpdateLearningGoalDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must be 200 characters or fewer.");

            When(x => x.IsCompleted, () =>
            {
                RuleFor(x => x.CompletedDate)
                    .NotNull().WithMessage("CompletedDate is required when marking a goal completed.")
                    .LessThanOrEqualTo(DateTime.Now).WithMessage("CompletedDate cannot be in the future.");
            });
        }
    }
}