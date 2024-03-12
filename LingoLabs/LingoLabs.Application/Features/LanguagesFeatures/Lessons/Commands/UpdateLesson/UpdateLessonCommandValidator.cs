using FluentValidation;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;

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

            RuleFor(p => p.UpdateLessonDto.LessonTitle)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p)
                .MustAsync((p, cancellation) => ValidateLesson(p.UpdateLessonDto.LessonTitle, lessonRepository, p.LessonId))
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(p => p.UpdateLessonDto.LessonDescription)
                .MaximumLength(500).When(p => !string.IsNullOrEmpty(p.UpdateLessonDto.LessonDescription))
                .WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(p => p.UpdateLessonDto.LessonRequirement)
                .MaximumLength(500).When(p => !string.IsNullOrEmpty(p.UpdateLessonDto.LessonRequirement))
                .WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(p => p.UpdateLessonDto.LessonContent)
                .MaximumLength(500).When(p => !string.IsNullOrEmpty(p.UpdateLessonDto.LessonContent))
                .WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(p => p.UpdateLessonDto.LessonVideoLink)
                .Must(BeValidUrl).When(p => !string.IsNullOrEmpty(p.UpdateLessonDto.LessonVideoLink))
                .WithMessage("{PropertyName} should be a valid URL if it exists.");

            RuleFor(p => p.UpdateLessonDto.LessonImageData)
                .Must(BeJpgOrPng).When(p => p.UpdateLessonDto.LessonImageData != null && p.UpdateLessonDto.LessonImageData.Length > 0)
                .WithMessage("{PropertyName} should be a .jpg or .png image if it exists.");
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

        private async Task<bool> ValidateLesson(string lessonTitle, ILessonRepository lessonRepository, Guid lessonId)
        {

            if (await lessonRepository.ExistsLessonForUpdateAsync(lessonTitle, lessonId) == true)
                return false;
            return true;
        }
    }
}
