using FluentValidation;
using LingoLabs.Domain.Entities;

namespace LingoLabs.Application.Features.LearningStyles.Commands.CreateLearningStyle
{
    public class CreateLearningStyleCommandValidator: AbstractValidator<CreateLearningStyleCommand>
    {
        public CreateLearningStyleCommandValidator()
        {
            RuleFor(p => p.LearningStyleName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.LearningType)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Must(type => type == LearningType.Auditory || type == LearningType.Visual || type == LearningType.Kinesthetic || type == LearningType.Logical)
                .WithMessage("{PropertyName} must have one of the following values: Auditory, Visual, Kinesthetic, Logical");
        }
    }
}
