using FluentValidation;

namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Commands.UpdateChapter
{
    public class UpdateChapterCommandValidator: AbstractValidator<UpdateChapterCommandCommand>
    {
        public UpdateChapterCommandValidator()
        {
            RuleFor(p => p.ChapterId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");

            RuleFor(p => p.UpdateChapterDto.ChapterName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}
