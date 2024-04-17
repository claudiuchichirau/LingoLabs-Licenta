using FluentValidation;
using LingoLabs.Application.Persistence.Languages;
using System.Net;

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
                .WithMessage("LanguageName must be unique.");

            RuleFor(p => p.LanguageFlag)
               .Must(BeImageValidUrl).When(p => !string.IsNullOrEmpty(p.LanguageFlag))
               .WithMessage("{PropertyName} should be a valid url.");
        }

        private async Task<bool> ValidateLanguage(string name, ILanguageRepository languageRepository)
        {
            if(await languageRepository.ExistsLanguageAsync(name))
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
                try
                {
                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        return response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase);
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Response is HttpWebResponse errorResponse)
                    {
                        // Handle specific HTTP error codes here, if needed
                    }
                    return false;
                }
            }

            return false;
        }

    }
}
