using FluentValidation;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.UpdateLanguageCompetence
{
    public class UpdateLanguageCompetenceCommandValidator: AbstractValidator<UpdateLanguageCompetenceCommand>
    {
        public UpdateLanguageCompetenceCommandValidator()
        {
            RuleFor(x => x.LanguageCompetenceId)
                .NotEmpty().WithMessage("Language competence id is required")
                .NotEqual(Guid.Empty).WithMessage("Language competence id is required");

            RuleFor(x => x.UpdateLanguageCompetenceDto.LanguageCompetenceDescription)
                .MaximumLength(500)
                .WithMessage($"Language competence description must be less than 500 characters");

            RuleFor(x => x.UpdateLanguageCompetenceDto.LanguageCompetenceVideoLink)
                .Must(BeValidUrl).When(p => !string.IsNullOrEmpty(p.UpdateLanguageCompetenceDto.LanguageCompetenceVideoLink))
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
