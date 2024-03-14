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

            RuleFor(p => p.UpdateLanguageLevelDto.LanguageLevelName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p)
                .MustAsync((p, cancellation) => ValidateLanguageLevelName(p.UpdateLanguageLevelDto.LanguageLevelName, languageLevelRepository, p.LanguageLevelId))
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(p => p.UpdateLanguageLevelDto.LanguageLevelAlias)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.UpdateLanguageLevelDto.LanguageLevelDescription)
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(p => p.UpdateLanguageLevelDto.LanguageLevelVideoLink)
                .Must(BeValidUrl).When(p => !string.IsNullOrEmpty(p.UpdateLanguageLevelDto.LanguageLevelVideoLink))
                .WithMessage("{PropertyName} should be a valid URL if it exists.");

            RuleFor(p => p.UpdateLanguageLevelDto.PriorityNumber)
                .GreaterThan(0).When(p => p.UpdateLanguageLevelDto.PriorityNumber.HasValue)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(p => p)
                .MustAsync((p, cancellation) => ValidatePriorityNumber(p.UpdateLanguageLevelDto.PriorityNumber.Value, p.LanguageLevelId, languageLevelRepository, languageRepository))
                .When(p => p.UpdateLanguageLevelDto.PriorityNumber.HasValue && p.UpdateLanguageLevelDto.PriorityNumber.Value > 0)
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
