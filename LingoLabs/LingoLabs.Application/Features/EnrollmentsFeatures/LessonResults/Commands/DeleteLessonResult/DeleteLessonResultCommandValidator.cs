using FluentValidation;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.DeleteLessonResult
{
    public class DeleteLessonResultCommandValidator: AbstractValidator<DeleteLessonResultCommand>
    {
        public DeleteLessonResultCommandValidator()
        {
            RuleFor(p => p.LessonResultId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
        }
    }
}
