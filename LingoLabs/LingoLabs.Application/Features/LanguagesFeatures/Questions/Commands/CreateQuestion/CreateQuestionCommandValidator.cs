using FluentValidation;
using LingoLabs.Domain.Entities;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommandValidator: AbstractValidator<CreateQuestionCommand>
    {
        public CreateQuestionCommandValidator()
        {
            RuleFor(p => p.QuestionRequirement)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(p => p.QuestionLearningType)
                .NotNull()
                .Must(type => type == LearningType.Auditory || type == LearningType.Visual || type == LearningType.Kinesthetic || type == LearningType.Logical)
                .WithMessage("{PropertyName} must have one of the following values: Auditory, Visual, Kinesthetic, Logical");

            RuleFor(p => p.LessonId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(default(System.Guid)).WithMessage("{PropertyName} is required.");
        }
    }
}
