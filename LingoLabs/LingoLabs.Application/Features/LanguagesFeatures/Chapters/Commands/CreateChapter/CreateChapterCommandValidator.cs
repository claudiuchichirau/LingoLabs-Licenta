using FluentValidation;

namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Commands.CreateChapter
{
    public class CreateChapterCommandValidator : AbstractValidator<CreateChapterCommand>
    {
        public CreateChapterCommandValidator()
        {
            RuleFor(p => p.ChapterName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}
