using FluentValidation;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using System.Net;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.UpdateLesson
{
    public class UpdateLessonCommandValidator: AbstractValidator<UpdateLessonCommand>
    {
        private readonly ILessonRepository lessonRepository;
        public UpdateLessonCommandValidator(ILessonRepository lessonRepository)
        {
            this.lessonRepository = lessonRepository;

            RuleFor(p => p.LessonId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");

            RuleFor(p => p.LessonTitle)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p)
                .MustAsync((p, cancellation) => ValidateLesson(p.LessonTitle, lessonRepository, p.LessonId))
                .WithMessage("LessonTitle must be unique.");

            RuleFor(p => p.LessonDescription)
                .MaximumLength(500).When(p => !string.IsNullOrEmpty(p.LessonDescription))
                .WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(p => p.LessonRequirement)
                .MaximumLength(500).When(p => !string.IsNullOrEmpty(p.LessonRequirement))
                .WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(p => p.LessonContent)
                .MaximumLength(500).When(p => !string.IsNullOrEmpty(p.LessonContent))
                .WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(p => p.LessonVideoLink)
                .Must(BeValidUrl).When(p => !string.IsNullOrEmpty(p.LessonVideoLink))
                .WithMessage("{PropertyName} should be a valid URL if it exists.");

            RuleFor(p => p.LessonImageData)
                .Must(BeImageValidUrl).When(p => !string.IsNullOrEmpty(p.LessonImageData))
                .WithMessage("{PropertyName} should be a .jpg or .png image if it exists.");

            RuleFor(p => p)
                .MustAsync((p, cancellation) => ValidatePriorityNumber(p.LessonPriorityNumber.Value, p.LessonId, lessonRepository))
                .When(p => p.LessonPriorityNumber.HasValue && p.LessonPriorityNumber.Value > 0)
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

        private async Task<bool> ValidateLesson(string lessonTitle, ILessonRepository lessonRepository, Guid lessonId)
        {

            if (await lessonRepository.ExistsLessonForUpdateAsync(lessonTitle, lessonId) == true)
                return false;
            return true;
        }

        private async Task<bool> ValidatePriorityNumber(int priorityNumber, Guid lessonId, ILessonRepository lessonRepository)
        {
            if (await lessonRepository.ExistsLessonPriorityNumberAsync(priorityNumber, lessonId) == true)
                return false;
            return true;
        }
    }
}
