using FluentValidation;
using LingoLabs.Application.Persistence.Languages;
using System.Net;

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

            RuleFor(p => p.ChapterName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p)
                .MustAsync((p, cancellation) => ValidateChapter(p.ChapterName, chapterRepository, p.ChapterId))
                .WithMessage("ChapterName must be unique.");
            
            RuleFor(p => p.ChapterDescription)
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(p => p)
                .MustAsync((p, cancellation) => ValidatePriorityNumber(p.ChapterPriorityNumber.Value, p.ChapterId, chapterRepository))
                .When(p => p.ChapterPriorityNumber.HasValue && p.ChapterPriorityNumber.Value > 0)
                .WithMessage("PriorityNumber must be unique.");

            RuleFor(p => p.ChapterVideoLink)
                .Must(BeValidUrl).When(p => !string.IsNullOrEmpty(p.ChapterVideoLink))
                .WithMessage("{PropertyName} should be a valid URL if it exists.");

            RuleFor(p => p.ChapterImageData)
                .Must(BeImageValidUrl).When(p => !string.IsNullOrEmpty(p.ChapterImageData))
                .WithMessage("{PropertyName} should be a valid url if it exists.");


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

        private static bool BeImageValidUrl(string url)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (result)
            {
                var request = WebRequest.Create(uriResult) as HttpWebRequest;
                request.Method = "HEAD";
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    return response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase);
                }
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
