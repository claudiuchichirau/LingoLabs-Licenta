using FluentValidation;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.UpdateLanguageCompetence
{
    public class UpdateLanguageCompetenceCommandValidator: AbstractValidator<UpdateLanguageCompetenceCommand>
    {
        private readonly ILanguageCompetenceRepository languageCompetenceRepository;
        public UpdateLanguageCompetenceCommandValidator(ILanguageCompetenceRepository languageCompetenceRepository)
        {
            this.languageCompetenceRepository = languageCompetenceRepository;

            RuleFor(x => x.LanguageCompetenceId)
                .NotEmpty().WithMessage("Language competence id is required")
                .NotEqual(Guid.Empty).WithMessage("Language competence id is required");

            RuleFor(x => x.LanguageCompetenceDescription)
                .MaximumLength(500)
                .WithMessage($"Language competence description must be less than 500 characters");

            RuleFor(x => x.LanguageCompetenceVideoLink)
                .Must(BeValidUrl).When(p => !string.IsNullOrEmpty(p.LanguageCompetenceVideoLink))
                .WithMessage("{PropertyName} should be a valid URL if it exists.");

            RuleFor(p => p)
                .MustAsync((p, cancellation) => ValidatePriorityNumber(p.LanguageCompetencePriorityNumber.Value, p.LanguageCompetenceId, languageCompetenceRepository))
                .When(p => p.LanguageCompetencePriorityNumber.HasValue && p.LanguageCompetencePriorityNumber.Value > 0)
                .WithMessage("PriorityNumber must be unique.");
        }

        private bool BeValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)
               && (uriResult.Host == "www.youtube.com" || uriResult.Host == "youtu.be");
        }

        private async Task<bool> ValidatePriorityNumber(int priorityNumber, Guid languageCompetenceId, ILanguageCompetenceRepository languageCompetenceRepository)
        {
            if (await languageCompetenceRepository.ExistsLanguageCompetencePriorityNumberAsync(priorityNumber, languageCompetenceId) == true)
                return false;
            return true;
        }
    }
}
