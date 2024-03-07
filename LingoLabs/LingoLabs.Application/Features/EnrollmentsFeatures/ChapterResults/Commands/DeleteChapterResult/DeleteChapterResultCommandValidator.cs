using FluentValidation;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Commands.DeleteChapterResult
{
    public class DeleteChapterResultCommandValidator: AbstractValidator<DeleteChapterResultCommand>
    {
        public DeleteChapterResultCommandValidator()
        {
            RuleFor(p => p.ChapterResultId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
        }
    }
}
