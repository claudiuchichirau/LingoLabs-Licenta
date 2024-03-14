using FluentValidation;
using LingoLabs.Application.Persistence.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Commands.UpdateChapter
{
    public class UpdateChapterCommandValidator: AbstractValidator<UpdateChapterCommandCommand>
    {
        private readonly IChapterRepository chapterRepository;
        public UpdateChapterCommandValidator(IChapterRepository chapterRepository)
        {
            this.chapterRepository = chapterRepository;

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
            
            RuleFor(p => p.UpdateChapterDto.ChapterDescription)
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(p => p)
                .MustAsync((p, cancellation) => ValidatePriorityNumber(p.UpdateChapterDto.ChapterPriorityNumber.Value, p.ChapterId, chapterRepository))
                .When(p => p.UpdateChapterDto.ChapterPriorityNumber.HasValue && p.UpdateChapterDto.ChapterPriorityNumber.Value > 0)
                .WithMessage("PriorityNumber must be unique.");

            RuleFor(p => p.UpdateChapterDto.ChapterVideoLink)
                .Must(BeValidUrl).When(p => !string.IsNullOrEmpty(p.UpdateChapterDto.ChapterVideoLink))
                .WithMessage("{PropertyName} should be a valid URL if it exists.");

            RuleFor(p => p.UpdateChapterDto.ChapterImageData)
                .Must(BeJpgOrPng).When(p => p.UpdateChapterDto.ChapterImageData != null && p.UpdateChapterDto.ChapterImageData.Length > 0)
                .WithMessage("{PropertyName} should be a .jpg or .png image if it exists.");


        }

        private async Task<bool> ValidateChapter(string name, IChapterRepository chapterRepository, Guid chapterId)
        {
            if (await chapterRepository.ExistChapterByNameForUpdateAsync(name, chapterId))
                return false;
            return true;
        }

        private async Task<bool> ValidatePriorityNumber(int priorityNumber, Guid chapterId, IChapterRepository chapterRepository)
        {
            if (await chapterRepository.ExistsChapterPriorityNumberAsync(priorityNumber, chapterId) == true)
                return false;
            return true;
        }

        private bool BeJpgOrPng(byte[] imageData)
        {
            var jpgHeader = new byte[] { 0xFF, 0xD8 };
            var pngHeader = new byte[] { 0x89, 0x50, 0x4E, 0x47 };

            if (imageData.Take(2).SequenceEqual(jpgHeader) || imageData.Take(4).SequenceEqual(pngHeader))
            {
                return true;
            }

            return false;
        }

        private bool BeValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)
               && (uriResult.Host == "www.youtube.com" || uriResult.Host == "youtu.be");
        }
    }
}
