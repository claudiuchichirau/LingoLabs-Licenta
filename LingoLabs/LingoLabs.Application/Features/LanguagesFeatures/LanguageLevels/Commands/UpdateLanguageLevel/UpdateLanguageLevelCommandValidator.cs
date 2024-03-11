using FluentValidation;
using LingoLabs.Application.Persistence.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Commands.UpdateLanguageLevel
{
    public class UpdateLanguageLevelCommandValidator: AbstractValidator<UpdateLanguageLevelCommand>
    {
        public UpdateLanguageLevelCommandValidator()
        {
            RuleFor(p => p.LanguageLevelId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");

            RuleFor(p => p.UpdateLanguageLevelDto.LanguageLevelAlias)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.UpdateLanguageLevelDto.LanguageLevelDescription)
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(p => p.UpdateLanguageLevelDto.LanguageLevelVideoLink)
                .Must(BeValidUrl).When(p => !string.IsNullOrEmpty(p.UpdateLanguageLevelDto.LanguageLevelVideoLink))
                .WithMessage("{PropertyName} should be a valid URL if it exists.");

        }
        private bool BeValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)
               && (uriResult.Host == "www.youtube.com" || uriResult.Host == "youtu.be");
        }
    }
}
