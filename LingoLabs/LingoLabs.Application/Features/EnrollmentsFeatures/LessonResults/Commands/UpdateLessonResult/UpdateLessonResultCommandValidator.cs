using FluentValidation;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.UpdateLessonResult
{
    public class UpdateLessonResultCommandValidator: AbstractValidator<UpdateLessonResultCommand>
    {
        public UpdateLessonResultCommandValidator()
        {
            RuleFor(x => x.LessonResultId)
                .NotEmpty().WithMessage("LessonResultId is required")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");

            RuleFor(x => x.IsCompleted)
                .NotNull().WithMessage("IsCompleted is required");

        }
    }
}
