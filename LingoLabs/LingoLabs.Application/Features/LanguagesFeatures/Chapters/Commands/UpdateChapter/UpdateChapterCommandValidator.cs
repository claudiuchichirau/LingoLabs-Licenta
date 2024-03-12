using FluentValidation;
using LingoLabs.Application.Persistence.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Commands.UpdateChapter
{
    public class UpdateChapterCommandValidator: AbstractValidator<UpdateChapterCommandCommand>
    {
        private readonly IChapterRepository chapterRepository;
        public UpdateChapterCommandValidator(IChapterRepository chapterRepository)
        {
            RuleFor(p => p.ChapterId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");

            RuleFor(p => p.UpdateChapterDto.ChapterName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p)
                .MustAsync((p, cancellation) => ValidateChapter(p.UpdateChapterDto.ChapterName, chapterRepository, p.ChapterId))
                .WithMessage("ChapterName must be unique.");
            this.chapterRepository = chapterRepository;
        }

        private async Task<bool> ValidateChapter(string name, IChapterRepository chapterRepository, Guid chapterId)
        {
            if (await chapterRepository.ExistChapterByNameForUpdateAsync(name, chapterId))
                return false;
            return true;
        }
    }
}
