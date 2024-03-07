using FluentValidation;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.UpdateLanguageCompetence
{
    public class UpdateLanguageCompetenceCommandValidator: AbstractValidator<UpdateLanguageCompetenceCommand>
    {
        public UpdateLanguageCompetenceCommandValidator(ILanguageCompetenceRepository repository)
        {
        }
    }
}
