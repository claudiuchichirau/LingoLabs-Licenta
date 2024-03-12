using FluentValidation;
using LingoLabs.Application.Persistence.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.CreateLanguage
{
    public class CreateLanguageCommandValidator : AbstractValidator<CreateLanguageCommand>
    {
        private readonly ILanguageRepository languageRepository;
        public CreateLanguageCommandValidator(ILanguageRepository languageRepository)
        {
            this.languageRepository = languageRepository;

            RuleFor(p => p.LanguageName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p)
                .MustAsync((p, cancellation) => ValidateLanguage(p.LanguageName, languageRepository))
                .WithMessage("{PropertyName} must be unique.");
        }

        private async Task<bool> ValidateLanguage(string name, ILanguageRepository languageRepository)
        {
            if(await languageRepository.ExistsLanguageAsync(name))
                return false;
            return true;
        }
    }
}
