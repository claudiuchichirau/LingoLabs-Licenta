using FluentValidation;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ReadingQuestionResults.Commands.CreateReadingQuestionResult
{
    public class CreateReadingQuestionResultCommandValidator: AbstractValidator<CreateReadingQuestionResultCommand>
    {
        public CreateReadingQuestionResultCommandValidator()
        {
            RuleFor(p => p.QuestionId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");

            RuleFor(p => p.LessonResultId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");

            RuleFor(p => p.IsCorrect)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.AudioData)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
