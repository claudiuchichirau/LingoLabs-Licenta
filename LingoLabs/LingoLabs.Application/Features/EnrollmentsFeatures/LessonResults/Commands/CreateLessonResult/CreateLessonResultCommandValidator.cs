using FluentValidation;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.CreateLessonResult
{
    public class CreateLessonResultCommandValidator: AbstractValidator<CreateLessonResultCommand>
    {
        public CreateLessonResultCommandValidator()
        {
            RuleFor(p => p.LessonId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");

            RuleFor(p => p.ChapterResultId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
        }
    }
}
