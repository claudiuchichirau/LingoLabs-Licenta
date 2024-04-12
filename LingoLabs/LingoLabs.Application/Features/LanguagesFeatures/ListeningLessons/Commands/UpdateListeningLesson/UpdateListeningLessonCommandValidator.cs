using FluentValidation;
using LingoLabs.Application.Persistence.Languages;
using System.Net;

namespace LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Commands.UpdateListeningLesson
{
    public class UpdateListeningLessonCommandValidator: AbstractValidator<UpdateListeningLessonCommand>
    {
        private readonly ILessonRepository lessonRepository;
        public UpdateListeningLessonCommandValidator(ILessonRepository lessonRepository)
        {
            this.lessonRepository = lessonRepository;

            RuleFor(p => p.LessonId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");

            RuleFor(p => p.UpdateListeningLessonDto.LessonTitle)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.UpdateListeningLessonDto.LessonDescription)
                .MaximumLength(500).When(p => !string.IsNullOrEmpty(p.UpdateListeningLessonDto.LessonDescription))
                .WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(p => p.UpdateListeningLessonDto.LessonRequirement)
                .MaximumLength(500).When(p => !string.IsNullOrEmpty(p.UpdateListeningLessonDto.LessonRequirement))
                .WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(p => p.UpdateListeningLessonDto.LessonContent)
                .MaximumLength(500).When(p => !string.IsNullOrEmpty(p.UpdateListeningLessonDto.LessonContent))
                .WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(p => p.UpdateListeningLessonDto.LessonVideoLink)
                .Must(BeValidUrl).When(p => !string.IsNullOrEmpty(p.UpdateListeningLessonDto.LessonVideoLink))
                .WithMessage("{PropertyName} should be a valid URL if it exists.");

            RuleFor(p => p.UpdateListeningLessonDto.LessonImageData)
                .Must(BeImageValidUrl).When(p => !string.IsNullOrEmpty(p.UpdateListeningLessonDto.LessonImageData))
                .WithMessage("{PropertyName} should be a .jpg or .png image if it exists.");

            RuleFor(p => p.UpdateListeningLessonDto.TextScript)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(1000).WithMessage("{PropertyName} must not exceed 1000 characters.");

            RuleFor(p => p.UpdateListeningLessonDto.Accents)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Must(accents => accents.Count >= 2).WithMessage("{PropertyName} must contain at least 2 elements.");

            RuleFor(p => p)
                .MustAsync((p, cancellation) => ValidatePriorityNumber(p.UpdateListeningLessonDto.LessonPriorityNumber.Value, p.LessonId, lessonRepository))
                .When(p => p.UpdateListeningLessonDto.LessonPriorityNumber.HasValue && p.UpdateListeningLessonDto.LessonPriorityNumber.Value > 0)
                .WithMessage("PriorityNumber must be unique.");

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

        private async Task<bool> ValidatePriorityNumber(int priorityNumber, Guid lessonId, ILessonRepository lessonRepository)
        {
            if (await lessonRepository.ExistsLessonPriorityNumberAsync(priorityNumber, lessonId) == true)
                return false;
            return true;
        }
    }
}
