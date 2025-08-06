// Validators/LearningGoals/CreateLearningGoalDtoValidator.cs
using FluentValidation;
using ThriveKid.API.DTOs.LearningGoals;

namespace ThriveKid.API.Validators.LearningGoals
{
    public class CreateLearningGoalDtoValidator : AbstractValidator<CreateLearningGoalDto>
    {
        public CreateLearningGoalDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must be 200 characters or fewer.");
            
            RuleFor(x => x.ChildId)
                .GreaterThan(0).WithMessage("A valid ChildId is required.");
        }
    }
}