using FluentValidation;
using LingoLabs.Application.Persistence.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Commands.CreateChapter
{
    public class CreateChapterCommandValidator : AbstractValidator<CreateChapterCommand>
    {
        private readonly IChapterRepository chapterRepository;
        public CreateChapterCommandValidator(IChapterRepository chapterRepository)
        {
            this.chapterRepository = chapterRepository;

            RuleFor(p => p.ChapterName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p)
                .MustAsync((p, cancellation) => ValidateChapter(p.ChapterName, chapterRepository))
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(p => p.LanguageLevelId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(default(System.Guid)).WithMessage("{PropertyName} is required.");
        }

        private async Task<bool> ValidateChapter(string name, IChapterRepository chapterRepository)
        {
            if(await chapterRepository.ExistChapterByNameAsync(name))
                return false;
            return true;
        }
    }
}
