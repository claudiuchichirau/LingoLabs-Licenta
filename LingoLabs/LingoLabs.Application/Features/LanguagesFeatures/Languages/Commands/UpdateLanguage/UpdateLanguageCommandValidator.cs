using FluentValidation;
using LingoLabs.Application.Persistence.Languages;
using System.Net;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.UpdateLanguage
{
    public class UpdateLanguageCommandValidator: AbstractValidator<UpdateLanguageCommand>
    {
        private readonly ILanguageRepository languageRepository;
        public UpdateLanguageCommandValidator(ILanguageRepository languageRepository)
        {
            this.languageRepository = languageRepository;

            RuleFor(p => p.LanguageId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be default.");

            RuleFor(p => p.UpdateLanguageDto.LanguageFlag)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Must(BeImageValidUrl).When(p => !string.IsNullOrEmpty(p.UpdateLanguageDto.LanguageFlag))
                .WithMessage("{PropertyName} should be a valid url if it exists.");

            RuleFor(p => p.UpdateLanguageDto.LanguageName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p)
                .MustAsync((p, cancellation) => ValidateLanguage(p.UpdateLanguageDto.LanguageName, languageRepository, p.LanguageId))
                .WithMessage("LanguageName must be unique.");

            RuleFor(p => p.UpdateLanguageDto.LanguageDescription)
                .MaximumLength(1000).WithMessage("{PropertyName} must not exceed 1000 characters.");

            RuleFor(p => p.UpdateLanguageDto.LanguageVideoLink)
                .Must(BeValidUrl).When(p => !string.IsNullOrEmpty(p.UpdateLanguageDto.LanguageVideoLink))
                .WithMessage("{PropertyName} should be a valid URL if it exists.");
        }

        private async Task<bool> ValidateLanguage(string name, ILanguageRepository languageRepository, Guid languageId)
        {

            if (await languageRepository.ExistsLanguageForUpdateAsync(name, languageId) == true)
                return false;
            return true;
        }

        private bool BeValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)
               && (uriResult.Host == "www.youtube.com" || uriResult.Host == "youtu.be");
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
    }
}
