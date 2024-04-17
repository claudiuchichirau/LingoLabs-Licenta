using FluentValidation;
using LingoLabs.Application.Persistence.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Commands.UpdateLanguageLevel
{
    public class UpdateLanguageLevelCommandValidator: AbstractValidator<UpdateLanguageLevelCommand>
    {
        private readonly ILanguageLevelRepository languageLevelRepository;
        private readonly ILanguageRepository languageRepository;
        public UpdateLanguageLevelCommandValidator(ILanguageLevelRepository languageLevelRepository, ILanguageRepository languageRepository)
        {
            this.languageLevelRepository = languageLevelRepository;
            this.languageRepository = languageRepository;

            RuleFor(p => p.LanguageLevelId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");

            RuleFor(p => p.LanguageLevelName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p)
                .MustAsync((p, cancellation) => ValidateLanguageLevelName(p.LanguageLevelName, languageLevelRepository, p.LanguageLevelId))
                .WithMessage("LanguageLevelName must be unique.");

            RuleFor(p => p.LanguageLevelAlias)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.LanguageLevelDescription)
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(p => p.LanguageLevelVideoLink)
                .Must(BeValidUrl).When(p => !string.IsNullOrEmpty(p.LanguageLevelVideoLink))
                .WithMessage("{PropertyName} should be a valid URL if it exists.");

            RuleFor(p => p.LanguageLevelPriorityNumber)
                .GreaterThan(0).When(p => p.LanguageLevelPriorityNumber.HasValue)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(p => p)
                .MustAsync((p, cancellation) => ValidatePriorityNumber(p.LanguageLevelPriorityNumber.Value, p.LanguageLevelId, languageLevelRepository, languageRepository))
                .When(p => p.LanguageLevelPriorityNumber.HasValue && p.LanguageLevelPriorityNumber.Value > 0)
                .WithMessage("PriorityNumber must be unique.");

        }
        private bool BeValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)
               && (uriResult.Host == "www.youtube.com" || uriResult.Host == "youtu.be");
        }

        private async Task<bool> ValidateLanguageLevelName(string languageLevelName, ILanguageLevelRepository languageLevelRepository, Guid languageLevelId)
        {

            if (await languageLevelRepository.ExistLanguageLevelUpdateAsync(languageLevelName, languageLevelId) == true)
                return false;
            return true;
        }

        private async Task<bool> ValidatePriorityNumber(int priorityNumber, Guid languageLevelId, ILanguageLevelRepository languageLevelRepository, ILanguageRepository languageRepository)
        {
            if (await languageRepository.ExistsLanguageLevelPriorityNumberAsync(priorityNumber, languageLevelId) == true)
                return false;
            return true;
        }
    }
}
