using FluentValidation;

namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Commands.DeleteChapter
{
    public class DeleteChapterCommandValidator: AbstractValidator<DeleteChapterCommand>
    {
        public DeleteChapterCommandValidator()
        {
            RuleFor(p => p.ChapterId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");
        }
    }
}
