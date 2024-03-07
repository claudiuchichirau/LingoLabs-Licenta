using FluentValidation;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Commands.UpdateChapterResult
{
    public class UpdateChapterResultCommandValidator: AbstractValidator<UpdateChapterResultCommand>
    {
        public UpdateChapterResultCommandValidator()
        {
            RuleFor(p => p.ChapterResultId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");
        }
    }
}
